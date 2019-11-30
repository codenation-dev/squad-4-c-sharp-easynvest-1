import React, { Component } from 'react';

export default class Header extends Component {
    

    render() {
        return (
            <div className="row header">
                <div className="col-lg-2 col-md-2 col-sm-2 col-2 logotipo">
                    <p>logo</p>
                </div>
                <div className="col-lg-8 col-md-8 col-sm-8 col-8 greeting">
                    <p className="Inconsolata-light">Olá, usuário. O seu token é: (Token).</p>
                    {/* Olá, {variable}. O seu token é: {variable2}. */}
                </div>
                <div className="col-lg-2 col-md-2 col-sm-2 col-2 logout">
                    <p className="Inconsolata-bold logout-btn"><i class="fas fa-sign-out-alt"></i></p>
                </div>
            </div>
        );
    }
}