import { useState } from 'react';

function FloatingInput({ label, type = 'text', icon, onValueChange }) {
    const [focused, setFocused] = useState(false);
    const [value, setValue] = useState('');

    const handleFocus = () => setFocused(true);
    const handleBlur = () => setFocused(value !== '');

    const handleChange = (e) => {
        const newValue = e.target.value;
        setValue(newValue);
        if (onValueChange) {
            onValueChange(newValue);
        }
    };

    return (
        <div className={`floating-label-group ${icon ? 'with-icon' : ''}`}>
            {icon && <i className={`input-icon ${icon}`}></i>}
            <input
                type={type}
                value={value}
                onChange={handleChange}
                onFocus={handleFocus}
                onBlur={handleBlur}
                className={`floating-input ${focused || value ? 'focused' : ''}`}
            />
            <label className={`floating-label ${focused || value ? 'focused' : ''}`}>
                {label}
            </label>
        </div>
    );
}

export default FloatingInput