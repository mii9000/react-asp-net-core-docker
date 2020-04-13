import React from 'react';
import GoogleLogin, {GoogleLoginResponse, GoogleLoginResponseOffline} from 'react-google-login';
import { AxiosResponse } from "axios";
import { useHistory } from "react-router-dom";
import { showToast, makeAxiosInstance } from "../utils";
import './Login.css';

interface GoogleErrorResponse {
    error: string
}

const googleLoginSuccess = (response: GoogleLoginResponse | GoogleLoginResponseOffline, history: any): void => {
    const body = { token: (response as GoogleLoginResponse).getAuthResponse().id_token };
    const axios = makeAxiosInstance();
    axios.post('/users/login', body)
    .then((axiosResponse: AxiosResponse<{ token: string }>) => {
        localStorage.setItem('token', axiosResponse.data.token);
        showToast('success', 'Login was successful')
        history.push('/');
    })
    .catch(reason => showToast('error', reason.toString()));
};

const googleLoginError = (response: GoogleErrorResponse) => {
    showToast('error', response.error.toString())
}

const Login = () => {
    const history = useHistory();
    return (
        <div className='center'>
            <GoogleLogin
                clientId="626059708897-jdfmn7erruscom7q7014vp57qnscscgc.apps.googleusercontent.com"
                buttonText="To continue, sign in with Google"
                onSuccess={response => googleLoginSuccess(response, history)}
                onFailure={googleLoginError}
                cookiePolicy={'single_host_origin'}
                theme="dark"
            />
        </div>
    )
}

export default Login
