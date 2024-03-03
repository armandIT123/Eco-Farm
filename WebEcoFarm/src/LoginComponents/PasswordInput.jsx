import React, { useState } from 'react'
import FloatingInput from '../Components/FloatingInput'

function PasswordInput({ email, hasAccount, onNext }) {

    const [password, setPassword] = useState('');

    const handlePasswordChange = (value) => {
        setPassword(value);
    }

    const handlePasswordVerification = () => {
        console.log(password);
    }

    return (
        <div className='password-input-container'>
            <i onClick={() => onNext('back')} className="bi bi-arrow-left-circle"></i>
            <h2>Bine ai venit rau ai nimerit</h2>
            <p>Utilizeaza parola pentru a te conecta la contul existent</p>
            <span>{email}</span>
            <p onClick={() => onNext('forgetPassword')}>Ai uitat parola?</p>
            <FloatingInput type='password' label='Parol/a' onValueChange={handlePasswordChange} icon='bi bi-lock' />

            <button className={password.length === 0 ? 'invalid-password' : ''} onClick={() => handlePasswordVerification()}>Conectare</button>
        </div>
    )
}

export default PasswordInput