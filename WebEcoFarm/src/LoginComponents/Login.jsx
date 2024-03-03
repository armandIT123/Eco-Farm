import { useState } from 'react'
import Modal from 'react-bootstrap/Modal';
import LoginMethod from './LoginMethod';
import EmailInput from './EmailInput'
import PasswordInput from './PasswordInput';
import ForgetPassword from './ForgetPassword';

import { useCheckEmailMutation } from '../Features/authApi';

export default function LoginModal(props) {

    const [email, setEmail] = useState('');
    const [hasAccount, setHasAccount] = useState(false);

    const [step, setStep] = useState(1);
    const [loginMethod, setLoginMethod] = useState('phoneNo')

    const [checkEmail, { isLoading: isChecking }] = useCheckEmailMutation();


    const handleSelectLoginMethod = (method) => {
        console.log(method);
        setLoginMethod(method);
        setStep(step + 1);
    }

    const handleOnNext = async (action, param) => {

        if (action === 'next') {
            switch (step) {
                case 2:

                    console.log(param);
                    try {
                        const data = await checkEmail({ param }).unwrap();
                        console.log('data');
                        console.log(data);
                    } catch (error) {
                        console.log(error);
                    }


                    setEmail(param);
                    // console.log('has account: ' + data);
                    //setHasAccount(data);
                    break;
            }
            setStep(step + 1);
        }
        else if (action === 'back')
            setStep(step - 1);
        else
            setLoginMethod(action);
    }

    return (
        <Modal
            {...props}
            size="md"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Body className='modal-body' >
                {step == 1 && <LoginMethod onSelectMethod={handleSelectLoginMethod} />}
                {step == 2 && loginMethod == 'email' && <EmailInput onNext={handleOnNext} />}
                {step == 3 && loginMethod == 'email' && <PasswordInput email={email} hasAccount={hasAccount} onNext={handleOnNext} />}
                {step == 3 && loginMethod == 'forgetPassword' && <ForgetPassword onNext={handleOnNext} />}
            </Modal.Body>
        </Modal>


    )
}