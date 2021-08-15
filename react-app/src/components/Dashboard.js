  
import React, { Component } from 'react';
import '../App.css';
import '../layouts/Main.css';

class Dashboard extends Component {
    constructor(props) {
        super(props);
        this.state = {
            item: 'default'
        };
    }

    fetchDonor = () => {        
        fetch(`https://localhost:44336/api/donor/1`, 
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
            <div>
                <p>Name of Donor: {this.state.item.firstName} {this.state.item.lastName}</p>
            </div>
        );
    }
}

export default Dashboard;