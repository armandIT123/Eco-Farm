import React, { useState } from 'react'
import FloatingInput from '../Components/FloatingInput'

import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';

function EmailInput({ onNext }) {

    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    const [email, setEmail] = useState('');
    const [invalidEmail, setInvalidEmail] = useState(true);

    const handleEmailChande = (value) => {
        setEmail(value);
        setInvalidEmail(!regex.test(value));
    }

    return (
        <div className='email-input-container'>
            <i onClick={() => onNext('back', '')} className="bi bi-arrow-left-circle"></i>
            <h2>S/a /incepem cu adresa ta de email</h2>
            <p>Vom verifica dac/a ai deja un cont. Dac/a nu, /iti vom crea unul</p>
            <FloatingInput label='E-mail' type='email' icon='bi bi-envelope' onValueChange={handleEmailChande} />
            <button className={invalidEmail ? 'invalid-continue-button' : ''} onClick={() => onNext('next', email)}>
                Continu/a
            </button>
        </div>
    )
}

export default EmailInput