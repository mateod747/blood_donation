  
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
                <p>Name of Donor: {this.state.item.firstName} {this.state.item.lastName}</p>
            </div>
        );
    }
}

export default Dashboard;