import React from 'react'
import FloatingInput from '../Components/FloatingInput'

import exitImg from "../../public/exit.png"
import { useLogOutMutation } from '../Features/authApi';

function Profile({ name, email, phoneNo }) {

    const [logout, { isLoading: isChecking }] = useLogOutMutation();

    return (
        <div className='profile-container'>
            <FloatingInput label='Nume' predefinedValue={name} />
            <FloatingInput label='E-mail' predefinedValue={email} />
            <FloatingInput label='Număr de telefon' predefinedValue={phoneNo} />

            <div className='logout-btn' onClick={() => logout()}>
                <img src={exitImg} height={16} />
                <span>Deconectare</span>
            </div>
        </div>
    )
}

export default Profile