
import React, { Component } from 'react';
import '../App.scss';
import '../layouts/DonationList.css';
import Pagination from 'react-bootstrap/Pagination';
import PageItem from 'react-bootstrap/PageItem';

let items = [];

class DonationList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            isLoaded: false,
            currentPage: [],
            paginationItems: [],
            active: 1
        };
    }


    async fetchDonationList(page) {
        let donorID = sessionStorage.getItem('donorID');
        await fetch(`https://localhost:44336/api/donationlist?page=${page}&pageSize=${3}&id=${donorID}`,
            {
                method: 'GET',
                headers: {
                    "Access-Control-Allow-Origin": "*",
                    "token": sessionStorage.getItem('loginToken')
                }
            })
            .then(async res => await res.json())
            .then(json => {
                this.setState({
                    isLoaded: true,
                    currentPage: json
                });
                for (let number = 1; number <= Math.ceil(this.state.currentPage.listCount / 3); number++) {
                    items.push(
                        <Pagination.Item onClick={} key={number} active={number === this.state.active}>
                          {number}
                        </Pagination.Item>
                    );
                };
                this.setState({
                    paginationItems: items
                });
            });
    }

    async componentDidMount() {
        await this.fetchDonationList(1);

        this.setState({
            paginationItems: items
        });
    }

    render() {
        if(!this.state.isLoaded) {
            return (null);
        }
        return (
            <div className="dashboard">
                <div className="dashboard-body">
                    <div className="cards">
                        {this.state.currentPage.donations.map(item => {
                            return (<div>{item.dateDonated}</div>)
                        })}
                    </div>
                    <div className="pagination">
                        <Pagination>{this.state.paginationItems}</Pagination>
                    </div>
                </div>
            </div>
        );
    }
}

export default DonationList;