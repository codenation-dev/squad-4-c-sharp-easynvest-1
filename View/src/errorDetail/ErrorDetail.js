import React, { Component } from 'react';
import BodyError from './BodyError.js';
import Header from '../home/Header.js';

class ErrorDetail extends Component {
    render() {
        return (
            <div className="">
                <Header />
                <BodyError />
            </div>
        );
    }
}

export default ErrorDetail;