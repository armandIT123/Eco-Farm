import { useRef, useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'

import { useDispatch } from 'react-redux'
import { setCredentials } from '../Features/authSlice'
import { useLoginMutation } from '../Features/authApiSlice'

function Login() {

    const userRef = useRef();
    const errRef = useRef();
    const [user, setUser] = useState('');
    const [pwd, setPwd] = useState('');
    const [errMsg, setErrMsg] = useState('');
    const navigate = useNavigate();

    const [login, { isLoading }] = useLoginMutation();
    const dispatch = useDispatch();

    useEffect(() => {
        userRef.current.focus();
    }, [])

    useEffect(() => {
        setErrMsg('');
    }, [user, pwd])

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            const userData = await login({ user, pwd }).unwrap();
            dispatch(setCredentials({ ...userData, user }));
            setUser('');
            setPwd('');
            navigate('/');
        } catch (err) {
            if (!err?.originalStatus)
                setErrMsg('No server error');
            else if (err.originalStatus?.status === 400)
                setErrMsg('Missing username or password');

            errRef.current.focus();
        }
    }

    const handleUserInput = (e) => setUser(e.target.value);
    const handlePwdInput = (e) => setPwd(e.target.value);

    return (
        <div>Login</div>
    )
}

export default Login