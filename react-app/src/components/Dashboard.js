
import React, { Component } from 'react';
import '../App.scss';
import '../layouts/Main.css';
import zero_plus from '../public/blood-types/0+.svg';
import OverlayTrigger from 'react-bootstrap/OverlayTrigger'
import Popover from 'react-bootstrap/Popover'
import { ProgressBar } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';

const popover = (
    <Popover id="popover-basic">
        <Popover.Header className="popover_header" as="h3">Trenutna zaliha</Popover.Header>
        <Popover.Body className="popover_body">
            <ProgressBar striped variant="danger" now={80} />
        </Popover.Body>
    </Popover>
);

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
                <div className="dashboard-body">
                </div>
                <div className="name">
                    Nadzorna ploča
                </div>
                <div className="header">
                    {this.state.item.firstName} {this.state.item.lastName}
                </div>
                <div className="header">
                    <div className="line"></div>
                </div>
                <div className="dash">
                    <div className="card-layout">
                        <div className="section section1">
                            <div className="section-area1" >
                                <h3>Krvna grupa</h3>
                                <OverlayTrigger trigger={['hover', 'focus']} placement="right" overlay={popover}>
                                    <img src={zero_plus} width="150px" height="150px" className="filters" alt="" />
                                </OverlayTrigger>
                            </div>
                        </div>
                        <div className="section section2">
                            <div className="section-area2" >
                                <Card bg="success">
                                    <Card.Header><h4>Podaci</h4></Card.Header>
                                    <ListGroup className="list" variant="flush">
                                        <ListGroup.Item variant="success">Dob: 23 | Spol: M</ListGroup.Item>
                                        <ListGroup.Item variant="success">Mjesto prebivališta: Hrv.branitelja 64, Ruščica 35208</ListGroup.Item>
                                        <ListGroup.Item variant="success">E-mail: mateod747@gmail.com</ListGroup.Item>
                                        <ListGroup.Item variant="success">Mob: +385 953938168</ListGroup.Item>
                                    </ListGroup>
                                </Card>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Dashboard;