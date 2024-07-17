import React, { useEffect, useState } from 'react'
import FloatingInput from '../Components/FloatingInput'

function PasswordInput({ email, hasAccount, onNext, invalidPassword }) {

    const [password, setPassword] = useState('');
    const [errorMsg, setErrorMsg] = useState('');
    const handlePasswordChange = (value) => {
        setPassword(value);
    }

    useEffect(() => {
        if (invalidPassword) {
            setErrorMsg("Parolă incorectă. Încearcă din nou.");
        }
        else {
            setErrorMsg('');
        }
    }, [invalidPassword])

    return (
        <div className='password-input-container'
            onKeyDown={(e) => password.length > 0 && e.key == 'Enter' ? onNext('next', password) : null}>

            <i onClick={() => onNext('back')} className="bi bi-arrow-left-circle"></i>
            <h2>Bine ai venit rau ai nimerit</h2>
            <p>{hasAccount ? 'Utilizeaza parola pentru a te conecta la contul existent' : 'Creaz/a un cont nou'}</p>
            <span>{email}</span>
            <p onClick={() => onNext('forgetPassword')}>Ai uitat parola?</p>
            <FloatingInput type='password' label='Parolă' onValueChange={handlePasswordChange} icon='bi bi-lock' />
            <p>{errorMsg}</p>
            <button className={password.length === 0 ? 'invalid-password' : ''} onClick={() => onNext('next', password)}>
                {hasAccount ? 'Conectare' : 'Continuă'}
            </button>
        </div>
    )
}

export default PasswordInput