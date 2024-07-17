import React, { useEffect, useState } from 'react'
import { useSelector } from 'react-redux';
import CartItemPreview from '../Components/CartItemPreview';
import { usePlaceOrderMutation } from '../Features/orderApi';
import { selectCurrentUser } from '../Features/authSlice';
import FloatingInput from '../Components/FloatingInput';

import Select from 'react-select';
import { Spinner } from 'react-bootstrap';
import { useGetSupplierNameQuery } from '../Features/suppliersApi';
import useSlug from '../Utilities/Navigation';
import { Link } from 'react-router-dom';
import GoogleMap from '../AdressComponents/GoogleMap';
import AdressModal from '../AdressComponents/AdressModal';

export default function Cart() {

    const [supplierId, setSupplierId] = useState(-1);
    const [paymentMethod, setPaymentMethod] = useState(-1);
    const [message, setMessage] = useState('');
    const [subTotal, setSubTotal] = useState(0);
    const [showModal, setShowModal] = useState(false);

    const user = useSelector(selectCurrentUser);
    const cart = useSelector((state) => state.cart);
    const [placeOrder, { isLoading: isLoading }] = usePlaceOrderMutation();
    const { data, error } = useGetSupplierNameQuery(supplierId);

    const PlaceOrder = () => {
        try {

            if (paymentMethod < 0)
                return;

            const order = {
                UserId: user.id,
                Products: cart.cartItems.map(item => ({
                    ProductId: item.id,
                    Quantity: item.quantity,
                    TotalAmount: (item.price * item.quantity).toFixed(2),
                })),
                DeliveryAddress: "",
                Message: message,
                PaymentMethod: +paymentMethod,
                Status: 0, //pending
            };
            placeOrder(order);

        } catch (error) {
            console.log(error);
        }
    }

    const handleAdressObtained = (adress) => {
        console.log(adress);
    }

    const paymentOptions = [
        { value: '0', label: 'Google Pay', image: 'google-pay.png' },
        { value: '2', label: 'Plată numerar', image: 'money.png' },
        { value: '3', label: 'Adaugă card', image: 'credit-card.png' },
    ];

    const handleMessageChande = (value) => {
        setMessage(value);
    }

    const handleChange = (selectedOption) => {
        setPaymentMethod(selectedOption.value);
    };

    useEffect(() => {
        console.log("data", error);
    }, [error])

    useEffect(() => {
        const subtotal = cart.cartItems?.reduce((accumulator, product) => {
            return accumulator + (product.quantity * product.price);
        }, 0) ?? 0;
        setSubTotal(subtotal);

        try {
            const id = cart.cartItems?.[0]?.supplierId
            if (supplierId !== id) {
                setSupplierId(id);
            }
        } catch (error) {

        }

    }, [cart])

    useEffect(() => {

    }, [])

    const customStyles = {
        control: (provided, state) => ({
            ...provided,
            // Changes the border color on focus
            borderColor: state.isFocused ? 'green' : provided.borderColor,
            // You can also use boxShadow to change the focus indicator
            boxShadow: state.isFocused ? 'green' : provided.boxShadow,
            // To change background color on focus or blur
            backgroundColor: state.isFocused ? 'white' : provided.backgroundColor
        })
        // to be modified
    };

    return (
        <>
            {isLoading && <div className='spinner-overlay'><Spinner /></div>}

            <div className='cart-page-container'>
                <div className='cart-page-first-column'>
                    <div className='cart-page-products-container'>
                        <h1>{data?.supplierName}</h1>
                        {cart.cartItems?.map((product, index) => (
                            <div key={index} className='cart-page-item-preview'>
                                <CartItemPreview product={product} />
                            </div>
                        ))}
                        <div className='cart-page-add-more'>
                            <Link to={"/descopera/" + useSlug(data?.supplierName) + "/" + supplierId}>Adaugă produse</Link>
                        </div>
                    </div>
                    <FloatingInput type='text-area' label={"Mențiuni suplimentare"} onValueChange={handleMessageChande} />
                    <div className='delivery-details'>
                        <h1>Detalii de livrare</h1>
                        <div onClick={() => setShowModal(true)}>
                            <GoogleMap onAddressObtained={handleAdressObtained} />
                        </div>
                        <span onClick={() => { console.log("ce") }}>
                            <FloatingInput type='text-area' label={"Număr de telefon"} />
                        </span>
                    </div>
                    <div className='payment-method'>
                        <h1>Metodă de plată</h1>
                        <Select
                            options={paymentOptions}
                            onChange={handleChange}
                            styles={customStyles}
                            formatOptionLabel={paymentOption => (
                                <div className="payment-option">
                                    <img src={paymentOption.image} style={{ height: '16px' }} alt="payment-option" />
                                    <span>{paymentOption.label}</span>
                                </div>
                            )}
                        />
                        <FloatingInput type='text' label={"Ai un cod de reducere?"} />
                    </div>
                </div>


                <div className='cart-page-summary'>
                    <div>
                        <h2>Sumar</h2>
                        <img src="/shopping-bag.png" style={{ height: '32px' }} />
                    </div>
                    <div>
                        <p>Produse</p>
                        <p>{subTotal.toFixed(2) + " lei"}</p>
                    </div>
                    <div>
                        <p>Livrare</p>
                        <p>15 lei</p>
                    </div>
                    <div>
                        <p>TOTAL</p>
                        <p>{(subTotal + 15).toFixed(2)}</p>
                    </div>
                    <button className='place-order-btn' onClick={() => PlaceOrder()}>Confirma Comanda</button>
                </div>
                <button className='place-order-btn-fixed' onClick={() => PlaceOrder()}>Confirma Comanda</button>
            </div>

            <AdressModal show={showModal}
                onHide={() => setShowModal(false)} />
        </>
    )
}