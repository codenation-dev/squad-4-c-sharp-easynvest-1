import React, { Component } from 'react';
import BodyLogin from './BodyLogin.js';
import Footer from './Footer.js';

class Login extends Component {
    render() {
        return (

            <div className="Login">
                    <BodyLogin />
                    <Footer />
            </div>
        );
    }
}

export default Login;