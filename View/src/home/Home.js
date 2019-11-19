import React, { Component } from 'react';
import BodyHome from './BodyHome.js';
import Header from './Header.js';

class Home extends Component {
    render() {
        return (
            <div className="">
                <Header />
                <BodyHome />
            </div>
        );
    }
}

export default Home;