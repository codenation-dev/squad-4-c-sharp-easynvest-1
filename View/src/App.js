import React, { Component } from 'react';
// import Register from './register/Register.js';
// import Login from './login/Login.js';
import Routes from './Routes.js';
import './index.css';

class App extends Component {
    render() {
        return ( 
            <div className="App">
                <Routes />
            </div>
        );
    }
}

export default App;