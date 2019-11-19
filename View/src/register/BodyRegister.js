import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import imgTela from './assets/imgTela.png';
import logo from './assets/logo.png';

export default class BodyRegister extends Component {
    render() {
        return (
                <div className="login-body">

                    <div className="row">
                        <div className="col-xs-12 col-lg-6 background-ivory login-mockup">
                            <img src={imgTela} className="img-fluid img-tela mx-auto d-block" alt="" />
                        </div>

                        <div className="col-xs-12 col-lg-6 text-center">

                            <img src={logo} className="img-fluid img-logo mx-auto d-block" alt="" />
                            <h1>Central de Erros</h1>
                            <h2>Crie sua conta!</h2>

                            <form>
                                <div className="text-left">
                                    <label>E-mail</label>
                                    <input type="email" id="email" aria-describedby="emailHelp" placeholder="Insira o seu e-mail"></input>
                                </div>
                                <div className="text-left">
                                    <label>Senha</label>
                                    <input type="password" id="senha" placeholder="Insira a sua senha"></input>
                                </div>
                                <NavLink exact to={`/login`}>Já tenho cadastro e gostaria de entrar</NavLink>
                                <button type="submit" className="button">Enviar</button>
                            </form>
                        </div>

                    </div>
                </div>
        );
    }
}