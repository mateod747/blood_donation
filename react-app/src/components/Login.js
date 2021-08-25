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
        .then(donorData => {
            sessionStorage.setItem('loginToken', donorData.Token !== 'null' ? donorData.Token : '');
            sessionStorage.setItem('donorID', donorData.DonorID);
            sessionStorage.setItem('admin', donorData.Admin);
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
        if (sessionStorage.getItem('loginToken').length > 0 && sessionStorage.getItem('admin') === "false") {
            window.location.href = 'http://localhost:3000/dashboard';
        }
        if (sessionStorage.getItem('loginToken').length > 0 && sessionStorage.getItem('admin') === "true") {
            window.location.href = 'http://localhost:3000/adminpage';
        }
    }
    return (
        <div className="box">
            <h2>Weekly Coding Challenge #1: Sign in/up Form</h2>
            <div class="container" id="container">
                <div class="form-container sign-in-container">
                    <form onSubmit={handleSubmit}>
                        <h1>Sign in</h1>
                        <input onChange={e => setUserName(e.target.value)} type="username" placeholder="Username" />
                        <input onChange={e => setPassword(e.target.value)} type="password" placeholder="Password" />
                        <button type="submit">Sign In</button>
                    </form>
                </div>
                <div class="overlay-container">
                    <div class="overlay">
                        <div class="overlay-panel overlay-left">
                            <h1>Welcome Back!</h1>
                            <p>To keep connected with us please login with your personal info</p>
                            <button class="ghost" id="signIn">Sign In</button>
                        </div>
                        <div class="overlay-panel overlay-right">
                            <h1>Hello, Friend!</h1>
                            <p>Enter your personal details and start journey with us</p>
                            <button class="ghost" id="signUp">Sign Up</button>
                        </div>
                    </div>
                </div>
            </div>

            <footer>
                <p>
                    Created with <i class="fa fa-heart"></i> by
                    <a target="_blank" href="https://florin-pop.com">Florin Pop</a>
                    - Read how I created this and how you can join the challenge
                    <a target="_blank" href="https://www.florin-pop.com/blog/2019/03/double-slider-sign-in-up-form/">here</a>.
                </p>
            </footer>
        </div>
    );
}

Login.propTypes = {
    setToken: PropTypes.func.isRequired
};
