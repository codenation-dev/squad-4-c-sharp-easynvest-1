import React from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import Register from './register/Register.js';
import Login from './login/Login.js';
import Home from './home/Home.js';
import ErrorDetail from './errorDetail/ErrorDetail.js';
import './index.css';

const Routes = () => (
    <Router>
        <Switch>
            <Route exact path='/' component={Register} />
            <Route path='/login' component={Login} />
            <Route path='/register' component={Register} />
            <Route path='/home' component={Home} />
            <Route path='/error-detail' component={ErrorDetail} />
        </Switch>
    </Router>
);

export default Routes;

