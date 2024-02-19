import { useEffect, useState } from "react";

const SIZES = ['qc-big', 'qc-small']

export default function QuantityControl({ onQuantityChange, size, startValue = 1, canDelete = false }) {

    const checkSize = SIZES.includes(size) ? size : SIZES[0];
    const [quantity, setQuantity] = useState(startValue);

    const handleButtonClick = (event) => {
        event.stopPropagation();

        if (event.target.className === '+') {
            setQuantity(quantity + 1);
            onQuantityChange(quantity + 1);
        }
        else if (event.target.className === '-') {
            if (quantity === 1) {
                onQuantityChange(0);
            }
            else {
                setQuantity(quantity - 1);
                onQuantityChange(quantity - 1);
            }
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