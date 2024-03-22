import { useState } from 'react'
import Modal from 'react-bootstrap/Modal';
import LoginMethod from './LoginMethod';
import EmailInput from './EmailInput'
import PasswordInput from './PasswordInput';
import ForgetPassword from './ForgetPassword';

import { useLoginMutation, useRegisterMutation, useCheckEmailMutation } from '../Features/authApi';
import NameInput from './NameInput';
import { useDispatch, useSelector } from 'react-redux';
import { closeModal } from '../Features/loginModalSlice';

export default function LoginModal() {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [invalidPassword, setInvalidPassword] = useState(false);
    const [hasAccount, setHasAccount] = useState(false);
    const [step, setStep] = useState(1);
    const [loginMethod, setLoginMethod] = useState('phoneNo')

    const showModal = useSelector((state) => state.loginModal.showModal);
    const dispatch = useDispatch();

    const [checkEmail, { isLoading: isChecking }] = useCheckEmailMutation();
    const [register, { isLoading: isRegisterLoading }] = useRegisterMutation();
    const [login, { isLoading: isLoginLoading }] = useLoginMutation();

    const handleSelectLoginMethod = (method) => {
        setLoginMethod(method);
        setStep(step + 1);
    }

    const handleOnHide = () => {
        dispatch(closeModal());
    }

    const handleOnNext = async (action, param) => {
        setInvalidPassword(false);
        if (action === 'next') {
            switch (step) {
                case 2:
                    try {
                        const data = await checkEmail({ email: param }).unwrap();
                        setHasAccount(data);
                    } catch (error) {
                        console.log(error);
                    }
                    setEmail(param);
                    setStep(step + 1);
                    break;

                case 3:
                    if (hasAccount) {
                        try {
                            await login({ email: email, password: param }).unwrap();
                            handleOnHide();
                            setStep(1);
                        } catch (error) {
                            setInvalidPassword(true);
                        }
                    }
                    else {
                        setPassword(param);
                        setStep(step + 1);
                    }
                    break;

                case 4:
                    try {
                        await register({ name: param, email: email, password: password }).unwrap();
                        setPassword("");
                        setStep(1);
                        handleOnHide();
                    }
                    catch (error) {

                    }
                    break;
            }
        }
        else if (action === 'back')
            setStep(step - 1);
        else
            setLoginMethod(action);
    }

    return (
        <Modal
            show={showModal}
            onHide={handleOnHide}
            size="md"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Body className='modal-body' >
                {step == 1 && <LoginMethod onSelectMethod={handleSelectLoginMethod} />}
                {step == 2 && loginMethod == 'email' && <EmailInput onNext={handleOnNext} />}
                {step == 3 && loginMethod == 'email' && <PasswordInput email={email} hasAccount={hasAccount} onNext={handleOnNext} invalidPassword={invalidPassword} />}
                {step == 3 && loginMethod == 'forgetPassword' && <ForgetPassword onNext={handleOnNext} />}
                {step == 4 && <NameInput onNext={handleOnNext} />}
            </Modal.Body>
        </Modal>
    )
}