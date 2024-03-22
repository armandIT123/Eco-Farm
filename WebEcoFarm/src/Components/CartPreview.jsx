import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import CartItemPreview from './CartItemPreview';
import { selectCurrentUser } from '../Features/authSlice';
import { openModal } from '../Features/loginModalSlice';


export default function CartPreview() {

    const cart = useSelector((state) => state.cart);
    const [subTotal, setSubTotal] = useState(0);
    const [count, setCount] = useState(0);
    const [wait4Login, setWait4Login] = useState(false);

    const user = useSelector(selectCurrentUser);
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const GoToCart = () => {
        if (user === null) {
            dispatch(openModal());
            setWait4Login(true);
        }
        else
            navigate('/cos');
    }

    useEffect(() => {
        if (wait4Login && user !== null) {
            navigate('/cos');
        }
    }, [user])

    useEffect(() => {
        const cartSummary = cart.cartItems.reduce((accumulator, product) => {
            if (!accumulator.uniqueProductIds.includes(product.id)) {
                accumulator.uniqueProductIds.push(product.id);
                accumulator.count += 1;
            }
            accumulator.subtotal += product.quantity * product.price;
            return accumulator;
        }, { subtotal: 0, count: 0, uniqueProductIds: [] });

        setSubTotal(cartSummary.subtotal);
        setCount(cartSummary.count);
    }, [cart])

    return (
        <>
            <div className='cart-preview-container'>
                <h4>Comandă curentă</h4>

                {cart.cartItems?.map((product, index) => (
                    <CartItemPreview key={index} product={product} />
                ))}
                <button className="cart-preview-button" onClick={() => GoToCart()}>
                    Comandă {count} pentru {subTotal.toFixed(2) + " lei"}
                </button>
            </div>
            <button className="cart-preview-fixed-button" onClick={() => GoToCart()}>
                Comandă {count} pentru {subTotal.toFixed(2) + " lei"}
            </button>
        </>
    )
}