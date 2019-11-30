import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

class BodyError extends Component {
    constructor(props) {
        super(props);
        this.goBack = this.goBack.bind(this);
    }

    goBack() {
        this.props.history.goBack();
    }

    render() {
        return (
            <div className="errors">
                <div className="row justify-content-start">
                    <div className="col-12">
                        <button className="back-btn" onClick={this.goBack}>
                            Voltar
                        </button>    
                    </div>
                </div>
                {/* a partir daqui: trocar por dinamicos */}
                <div className="row">
                    <div className="col-12">
                        <h1 className="error-headline Inconsolata-bold">Erro no /127.0.0.1/ em /24/05.2019 [10:15]/</h1>
                        <div className="col-12 text-center">
                            <button className="error-lvl label">Error</button>  
                        </div>
                    </div>
                </div>

                <div className="row">
                    <div className="col-lg-8">
                        <p>Título</p>
                        <p>Aqui vai o título do erro</p>
                    </div>
                    <div className="col-lg-4">
                        <p>Eventos</p>
                        <p>100</p>
                    </div>
                    <div className="col-lg-8">
                        <p>Detalhes</p>
                        <p>Aqui vão os detalhes do erroblablabalbalbal  balba labalbalalbalbla  ablabalalba alabalalbal alablab albalbla albalbalal ablalala</p>
                    </div>
                    <div className="col-lg-4">
                        <p>Coletado por</p>
                        <p>Token do usuário fulano</p>
                    </div>
                </div>


                {/* um título podendo ser o erro em si  */}
                {/* a descrição do erro */}
                {/* eventos */}
                {/* 'coletado por': o token de quem 'fez' o erro na api de erros */}
            </div>
        );
    }
}

export default withRouter(BodyError);