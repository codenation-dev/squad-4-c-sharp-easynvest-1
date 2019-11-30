import React, { Component } from 'react';

export default class Home extends Component {
    

    render() {
        return (
            <div className="errors">
                <div className="nav row d-flex align-items-center background-ivory">
                    <div className="col-lg-4 col-md-4 col-sm-3 col-6 holder text-center">
                        {/* trocar por um com map etc um dinamico */}
                        <div>
                            <select className="box background-ivory">
                                <option>Filtrar</option>
                                <option>Produção</option>
                                <option>Homologação</option>
                                <option>Desenvolvimento</option>
                            </select>
                        </div>
                    </div>
                    <div className="col-lg-4 col-md-4 col-sm-3 col-6 holder text-center">
                        {/* trocar por um com map etc um dinamico */}
                        <div>
                            <select className="box background-ivory">
                                <option>Ordernar</option>
                                <option>Level</option>
                                <option>Frequência</option>
                            </select>
                        </div>
                    </div>
                    <div className="col-lg-4 col-md-4 col-sm-6 col-12 holder text-center">
                        {/* dropdown searchbar - precisa ser a maior de todas*/}
                        {/* trocar por um dinamico e estilizar e incluir uma lupa dentro? */}
                            <div className="searchbox">
                                <input type="text" id="searchbar" className="search-bar background-ivory" aria-describedby="searchBar" placeholder="level, descrição, origem" alt="Busque por level, descrição ou origem" title="Busque por level, descrição ou origem"></input>
                                <i class="fab fa-searchengin"></i>
                            </div>
                    </div>
                </div>

                <div className="errorlist">
                    <div className="row error-header">
                        <h1 className="col-lg-4 col-md-4 col-sm-4 col-4 Inconsolata-bold">Level</h1>
                        <h1 className="col-lg-4 col-md-4 col-sm-4 col-4 text-center Inconsolata-bold">Log</h1>
                        <h1 className="col-lg-4 col-md-4 col-sm-4 col-4 text-right Inconsolata-bold">Eventos</h1>
                    </div>

                    <div className="row">
                        <div className="col-lg-4"><button className="error-lvl label">Error</button></div>
                        <div className="col-lg-4 text-center">Erro que rolou no blablablablalbalbalbalballbal</div>
                        <div className="col-lg-4 text-right">100</div>
                        <div className="col-lg-4"><button className="warning-lvl label">Warning</button></div>
                        <div className="col-lg-4 text-center">Erro que rolou no blablablablalbalbalbalballbal</div>
                        <div className="col-lg-4 text-right">40</div>
                        <div className="col-lg-4"><button className="debug-lvl label">Debug</button></div>
                        <div className="col-lg-4 text-center">Erro que rolou no blablablablalbalbalbalballbal</div>
                        <div className="col-lg-4 text-right">300</div>
                    </div>
                    
                    {/* embaixo vem os erros - a renderização das filtragens, com, do lado, ícones para apagar e arquivar (em cada erro) */}
                    {/* no log eu tenho que puxar a descrição, origem e a data (se for grande a desc, puxo o título? */}
                </div>
            </div>
        );
    }
}