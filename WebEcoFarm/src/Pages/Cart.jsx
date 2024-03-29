import React, { useEffect, useState } from 'react'
import { useSelector } from 'react-redux';
import CartItemPreview from '../Components/CartItemPreview';

export default function Cart() {

    const cart = useSelector((state) => state.cart);
    const [subTotal, setSubTotal] = useState(0);

    useEffect(() => {
        const subtotal = cart.cartItems.reduce((accumulator, product) => {
            return accumulator + (product.quantity * product.price);
        }, 0);
        setSubTotal(subtotal);
    }, [cart])

    return (
        <>
            <div className='cart-page-container'>

                <div className='cart-page-products-container'>
                    <h1>Supplier Name</h1>
                    {cart.cartItems.map((product, index) => (
                        <div className='cart-page-item-preview'>
                            <CartItemPreview key={index} product={product} />
                        </div>
                    ))}
                </div>


                <div className='cart-page-summary'>
                    <div>
                        <h2>Sumar</h2>
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
                    <button>Confirma Comanda</button>
                </div>
            </div>
        </>
    )
}