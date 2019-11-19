import React, { Component } from 'react';
import BodyRegister from './BodyRegister.js';
import Footer from './Footer.js';

class Register extends Component {
    render() {
        return (

            <div className="Login">
                    <BodyRegister />
                    <Footer />
            </div>
        );
    }
}

export default Register;