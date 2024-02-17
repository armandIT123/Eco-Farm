import { useState } from "react";

const SIZES = ['qc-big', 'qc-small']

export default function QuantityControl({ onQuantityChange, size }) {

    const checkSize = SIZES.includes(size) ? size : SIZES[0];

    const [quantity, setQuantity] = useState(1);

    const handleButtonClick = (event) => {
        event.stopPropagation();
        if (event.target.className === '+') {
            setQuantity(quantity + 1);
            onQuantityChange(quantity + 1);
        }
        else if (event.target.className === '-' && quantity > 1) {
            setQuantity(quantity - 1);
            onQuantityChange(quantity - 1);
        }
    };

    const handleChange = (event) => {
        const value = event.target.value;
        if (value === '') {
            setQuantity('');
        }
        else if (!isNaN(value) && parseInt(value) >= 1) {
            setQuantity(parseInt(value));
            onQuantityChange(value);
        }
    }

    const handleBlur = () => {
        if (quantity === '') {
            setQuantity(1);
            onQuantityChange(1);
        }
    };

    return (
        <div className={"quantity-control-container " + checkSize}>
            <button className="-" onClick={handleButtonClick}>-</button>
            <input onClick={(event) => event.stopPropagation()} value={quantity} onChange={handleChange} onBlur={handleBlur} />
            <button className="+" onClick={handleButtonClick}>+</button>
        </div>
    )
}