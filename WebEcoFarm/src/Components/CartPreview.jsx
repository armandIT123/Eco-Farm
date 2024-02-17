import React, { useState } from 'react'
import QuantityControl from './QuantityControl';
import { useSelector } from 'react-redux';

export default function CartPreview() {

    const cart = useSelector((state) => state.cart); //does this modify every Time I add something?



    return (
        <div className='cart-preview-container'>
            <h4>Comand/a curent/a</h4>

            {cart.cartItems?.map((product, index) => (
                <div className='cart-preview-product' key={index}>
                    <img src={"data:image/jpeg;base64," + product.image} alt={product.name} />
                    <div className='cart-preview-product-details'>
                        <h5>{product.name}</h5>
                        <div className='cart-preview-product-details-lower'>
                            <p>{product.price + " lei"}</p>
                            <QuantityControl size='qc-small' />
                        </div>
                    </div>
                </div>
            ))}

            <div>
                <p>Subtotal</p>
                <p></p>
                <p>Reducere</p>
                <p></p>

                <p>Total</p>
                <p></p>
            </div>
        </div>
    )
}