import React, { Component } from 'react';
import footer from './assets/footer.png';

export default class Footer extends Component {
    render() {
        return (

                <div className="login-footer">
                    <div className="">
                        <img src={footer} className="img-fluid img-footer mx-auto d-block" alt="" />
                    </div>
                </div>
        );
    }
}