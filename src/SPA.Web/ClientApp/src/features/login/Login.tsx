import React from 'react';
import GoogleLogin from 'react-google-login';
import Axios from "axios";

const responseGoogle = async (response: any) => {
    const ss = await Axios.post('/api/v1/auth/login', {
        token: response.getAuthResponse().id_token
    }, {
        headers: {
            'Content-Type': 'application/json'
        }
    })

    console.log(ss);
}  

const Login = () => {
    return (
        <GoogleLogin
            // get client id from backend
            clientId="626059708897-jdfmn7erruscom7q7014vp57qnscscgc.apps.googleusercontent.com"
            buttonText="Sign in with Google"
            onSuccess={responseGoogle}
            onFailure={responseGoogle}
            cookiePolicy={'single_host_origin'}
        />
    )
}

export default Login