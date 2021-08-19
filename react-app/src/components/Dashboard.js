
import React, { Component } from 'react';
import '../App.css';
import '../layouts/Main.css';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';


class Dashboard extends Component {
    constructor(props) {
        super(props);
        this.state = {
            item: 'default'
        };
    }

    fetchDonor = () => {
        let donorID = sessionStorage.getItem('donorID');
        fetch(`https://localhost:44336/api/donor/${donorID}`,
            {
                method: 'GET',
                headers: {
                    "Access-Control-Allow-Origin": "*",
                    "token": sessionStorage.getItem('loginToken')
                }
            })
            .then(res => res.json())
            .then(json => {
                this.setState({
                    item: json
                });
            });
    }

    componentDidMount() {
        this.fetchDonor();
    }

    render() {
        return (
            <div className="dashboard">
                <div className="dashboard-body ">
                </div>
                <link
                    rel="stylesheet"
                    href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css"
                    integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC"
                    crossorigin="anonymous"
                />
                <div className="name">
                    {this.state.item.firstName} {this.state.item.lastName}
                </div>
                <div className="header">
                    Osnovni podaci
                </div>
                <div className="header">
                    <div className="line"></div>
                </div>
                <div className="dash">
                    <div className="card">
                        <div className="section">

                        </div>
                        <div className="section">

                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Dashboard;