import { useState } from 'react'
import { useDispatch } from "react-redux";

import QuantityControl from './QuantityControl'
import { addToCart } from '../Features/cartSlice';

export default function ProductCard({ product, onClick }) {

    const [quantity, setQuantity] = useState(1);

    const dispatch = useDispatch();

    const handleAddToCart = (event) => {
        /*also need to add the quantity. Modify function*/
        event.stopPropagation();
        dispatch(addToCart(product));
    }

    const handleQuantityChange = (newQuantity) => {
        if (parseInt(newQuantity) >= 1)
            setQuantity(newQuantity);
    };

    return (
        <div className="product-card-container" onClick={onClick}>
            <img className="product-card-left" src={"data:image/jpeg;base64," + product.image} />
            <div className="product-card-right">
                <h4>{product.name}</h4>
                <h5>{product.price + " lei"}</h5>
                <QuantityControl onQuantityChange={handleQuantityChange} />
                <button className='products-card-add-to-cart' onClick={handleAddToCart}>Adaugă in coș</button>
            </div>
        </div>
    )
}