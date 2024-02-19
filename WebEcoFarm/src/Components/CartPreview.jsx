import React, { useEffect, useState } from 'react'
import QuantityControl from './QuantityControl';
import { useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { useDispatch } from "react-redux";
import CartItemPreview from './CartItemPreview';


export default function CartPreview() {

    const cart = useSelector((state) => state.cart);
    const [subTotal, setSubTotal] = useState(0);

    useEffect(() => {
        const subtotal = cart.cartItems.reduce((accumulator, product) => {
            return accumulator + (product.quantity * product.price);
        }, 0);
        setSubTotal(subtotal);
    }, [cart])


    return (
        <div className='cart-preview-container'>
            <h4>Comand/a curent/a</h4>

            {cart.cartItems?.map((product, index) => (
                <CartItemPreview key={index} product={product} />
            ))}

            <div className='cart-preview-summary'>
                <div className='cart-preview-summary-subtotal'>
                    <p>Subtotal</p>
                    <p>{subTotal.toFixed(2) + " lei"}</p>
                </div>
                <div className='cart-preview-summary-discount'>
                    <p>Reducere</p>
                    <p></p>
                </div>
                <div className='cart-preview-summary-total'>
                    <p>Total</p>
                    <p></p>
                </div>
                <Link to="/cos">
                    Continua catre cos
                </Link>
            </div>
        </div>
    )
}