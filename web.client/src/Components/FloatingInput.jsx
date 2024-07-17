import { useState } from 'react';
import EyeIcon from '../../public/eye.png';
import HiddenEyeIcon from '../../public/hidden-eye.png';

function FloatingInput({ label, type = 'text', icon, onValueChange, predefinedValue = '' }) {
    const [focused, setFocused] = useState(false);
    const [value, setValue] = useState(predefinedValue);
    const [inputType, setInputType] = useState(type);
    const [passwordVisible, setPasswordVisible] = useState(false);

    const handleFocus = () => setFocused(true);
    const handleBlur = () => setFocused(value !== '');

    const handleChange = (e) => {
        const newValue = e.target.value;
        setValue(newValue);
        if (onValueChange) {
            onValueChange(newValue);
        }
    };

    const togglePasswordVisibility = () => {
        setPasswordVisible(!passwordVisible);
        setInputType(passwordVisible ? 'password' : 'text');
    };

    return (
        <div className={`floating-label-group ${icon ? 'with-icon' : ''}`}>
            {icon && <i className={`input-icon ${icon}`}></i>}
            <input
                autoFocus
                type={inputType}
                value={value}
                onChange={handleChange}
                onFocus={handleFocus}
                onBlur={handleBlur}
                className={`floating-input ${focused || value ? 'focused' : ''}`}
            />
            {type === 'password' && (
                <img
                    src={passwordVisible ? HiddenEyeIcon : EyeIcon}
                    onClick={togglePasswordVisibility}
                    className="toggle-password"
                    alt="Toggle Password Visibility"
                />
            )}
            <label className={`floating-label ${focused || value ? 'focused' : ''}`}>
                {label}
            </label>
        </div>
    );
}

export default FloatingInput