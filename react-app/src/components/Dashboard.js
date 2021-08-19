
import React, { Component } from 'react';
import '../App.scss';
import '../layouts/Main.css';
import Button from 'react-bootstrap/Button';
import zero_plus from '../public/blood-types/0+.svg';
import OverlayTrigger from 'react-bootstrap/OverlayTrigger'
import Popover from 'react-bootstrap/Popover'
import { ProgressBar } from 'react-bootstrap';

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
                <div className="dashboard-body ">
                </div>
                <div className="name">
                    {this.state.item.firstName} {this.state.item.lastName}
                </div>
                <div className="header">
                    Nadzorna ploča
                </div>
                <div className="header">
                    <div className="line"></div>
                </div>
                <div className="dash">
                    <div className="card">
                        <div className="section section1">
                            <h3>Krvna grupa</h3>
                            <OverlayTrigger trigger="hover" placement="right" overlay={popover}>
                                <img src={zero_plus} width="150px" height="150px" class="filters"></img>
                            </OverlayTrigger>
                        </div>
                        <div className="section section2">
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Dashboard;