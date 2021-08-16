import React from 'react';
import '../layouts/Login.css';
import PropTypes from 'prop-types';

async function LoginUser(credentials) {
    return fetch('https://localhost:44336/api/login?username=' + credentials.username + '&password=' + credentials.password, 
    {
        method: 'GET',
        headers: {
            "Access-Control-Allow-Origin": "*"
        }
    })
        .then(response => response.json())
        .then(donorData =>
            {
                sessionStorage.setItem('loginToken', donorData.Token !== 'null' ? donorData.Token : '');
                sessionStorage.setItem('donorID', donorData.DonorID);
            }
        )
}

export default function Login() {
    const [username, setUserName] = React.useState();
    const [password, setPassword] = React.useState();

    const handleSubmit = async e => {
        e.preventDefault();
        await LoginUser({
            username,
            password
        })
        if(sessionStorage.getItem('loginToken').length > 0) {
            window.location.href='http://localhost:3000/dashboard';
        }
    }
    return (
        <div className="login-wrapper">
            <h1>Please Log In</h1>
            <form onSubmit={handleSubmit}>
                <label>
                    <p>Username</p>
                    <input type="text" onChange={e => setUserName(e.target.value)} />
                </label>
                <label>
                    <p>Password</p>
                    <input type="password" onChange={e => setPassword(e.target.value)} />
                </label>
                <div>
                    <button type="submit">Submit</button>
                </div>
            </form>
        </div>
    )
}

Login.propTypes = {
    setToken: PropTypes.func.isRequired
};
