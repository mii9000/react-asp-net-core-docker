import React from 'react';
import GoogleLogin, {GoogleLoginResponse, GoogleLoginResponseOffline} from 'react-google-login';
import Axios from "axios";
import { toast } from 'react-semantic-toasts';

interface GoogleErrorResponse {
    error: string
}

const googleLoginSuccess = (response: GoogleLoginResponse | GoogleLoginResponseOffline): void => {
    Axios.post('/api/v1/users/login', 
    { token: (response as GoogleLoginResponse).getAuthResponse().id_token }, 
    { headers: { 'Content-Type': 'application/json'} })
    .then(axiosResponse => 
        {
            showToast('success', 'Login was successful')
            console.log(axiosResponse);
        }).catch(reason => showToast('error', reason));
};

const showToast = (type: 'success' | 'error', desc: string) => {
    toast({
        type: type,
        title: type === 'success' ? 'Great news!' : 'Oops!',
        description: desc,
        animation: 'bounce',
        time: 1500
    });
}

const googleLoginError = (response: GoogleErrorResponse) => {
    showToast('error', response.error)
}

const Login = () => {
    return (
        <div className='center'>
            <GoogleLogin
                clientId="626059708897-jdfmn7erruscom7q7014vp57qnscscgc.apps.googleusercontent.com"
                buttonText="To continue, sign in with Google"
                onSuccess={googleLoginSuccess}
                onFailure={googleLoginError}
                cookiePolicy={'single_host_origin'}
                theme="dark"
            />
        </div>
    )
}

export default Login
