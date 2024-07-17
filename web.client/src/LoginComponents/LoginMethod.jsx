import React, { useState } from 'react'
import FloatingInput from '../Components/FloatingInput'

function LoginMethod({ onSelectMethod }) {

    const regex = /^(?:(?:(?:00\s?|\+)40\s?|0)(?:7\d{2}\s?\d{3}\s?\d{3}|(21|31)\d{1}\s?\d{3}\s?\d{3}|((2|3)[3-7]\d{1})\s?\d{3}\s?\d{3}|(8|9)0\d{1}\s?\d{3}\s?\d{3}))$/;
    const [phoneNo, setPhoneNo] = useState('');
    const [prefix, setPrefix] = useState('');
    const [invalidPhoneNo, setInvalidPhoneNo] = useState(false);
    const handlePhoneNoChange = (number) => {

    }

    const handleContinueBtn = () => {
        if (regex.test(phoneNo))
            onSelectMethod(prefix + phoneNo);
        else
            setInvalidPhoneNo(true);
    }

    return (
        <div className='login-container'>
            <h1>Bine ai venit</h1>
            <div className='login-phone'>
                <FloatingInput label='Prefix' />
                <FloatingInput label='Număr de telefon' onValueChange={handlePhoneNoChange} />
            </div>
            <button className='login-continue-button' onClick={() => handleContinueBtn()}>Continu/a</button>

            <div className="text-with-lines">
                <hr className="line" />
                <span className="text">sau cu</span>
                <hr className="line" />
            </div>

            <div className='login-other-methods' onClick={() => onSelectMethod('google')}>
                <img src='google.png' height={24} />
                <p>Google</p>
            </div>
            <div className='login-other-methods' onClick={() => onSelectMethod('email')}>
                <i className="bi bi-envelope" />
                <p>Email</p>
            </div>
        </div>
    )
}

export default LoginMethod