
import React, { Component } from 'react';
import '../App.scss';
import '../layouts/DonationList.css';

class DonationList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            item: 'default'
        };
    }


    componentDidMount() {
    }



    render() {
        return (
            <div className="dashboard">
                <div className="dashboard-body">

                </div>
            </div>
        );
    }
}

export default DonationList;