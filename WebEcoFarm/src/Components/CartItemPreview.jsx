import React from 'react'
import QuantityControl from './QuantityControl'
import { useDispatch } from 'react-redux';
import { addToCart, removeFromCart } from '../Features/cartSlice';

function CartItemPreview({ product }) {

    const dispatch = useDispatch();

    const handleQuantityChange = (newQuantity) => {
        const intQuantity = parseInt(newQuantity); // why would js jump from int to string? 0_x

        if (intQuantity >= 1) {
            const updatedProduct = { ...product, quantity: intQuantity };
            dispatch(addToCart(updatedProduct));
        }
        else if (intQuantity === 0) {
            dispatch(removeFromCart(product.id));
        }
    };

    return (
        <div className='cart-preview-product'>
            <img src={"data:image/jpeg;base64," + product.image} alt={product.name} />
            <div className='cart-preview-product-details'>
                <h5>{product.name}</h5>
                <div className='cart-preview-product-details-lower'>
                    <p>{product.price + " lei"}</p>
                    <QuantityControl onQuantityChange={handleQuantityChange} size='qc-small'
                        startValue={product.quantity} canDelete={true} />
                </div>
            </div>
        </div>
    )
}

export default CartItemPreview