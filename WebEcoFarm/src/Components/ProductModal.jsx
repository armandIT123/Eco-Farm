import { useState } from "react";
import { useDispatch } from "react-redux";
import { addToCart } from '../Features/cartSlice';

import Modal from 'react-bootstrap/Modal';
import QuantityControl from './QuantityControl';

export default function ProductModal(props) {

    const [quantity, setQuantity] = useState(1);
    const dispatch = useDispatch();

    const product = props.product;

    const handleQuantityChange = (newQuantity) => {
        if (parseInt(newQuantity) >= 1) {
            const intQuantity = parseInt(newQuantity);
            setQuantity(intQuantity);
        }
    };

    const handleAddToCart = () => {
        dispatch(addToCart({ ...product, quantity }));
        props.onHide();
    }

    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            scrollable={true}
        >

            <Modal.Body className='modal-body' >
                <i className="product-modal-close bi bi-x" onClick={() => { props.onHide() }}></i>
                <div className='product-modal-upper-part'>
                    <img className='product-modal-image' src={"data:image/jpeg;base64," + product.image} />
                    <div className='product-modal-upper-part-left'>
                        <h2>{product.name}</h2>
                        <h4>{(product.price * quantity).toFixed(2) + " lei"}</h4>
                        <p>{product.price + " lei/kg"}</p>
                        <QuantityControl onQuantityChange={handleQuantityChange} />
                        <button className='products-card-add-to-cart' onClick={handleAddToCart}>Adaugă in coș</button>
                    </div>
                </div>
                <div className='product-modal-lower-part'>
                    <h3>Descriere</h3>
                    <p>{product.description}</p>
                </div>

            </Modal.Body>
        </Modal>
    );
}