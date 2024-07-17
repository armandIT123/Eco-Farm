import React from 'react'

function ForgetPassword({ onNext }) {
    return (
        <div className='forget-password-container'>
            <i onClick={() => onNext('email')} className="bi bi-arrow-left-circle"></i>

        </div>
    )
}

export default ForgetPassword