import React, { Component } from 'react';
import '../App.scss';
import '../layouts/AdminPage.scss';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import Tabs from 'react-bootstrap/Tabs';
import Tab from 'react-bootstrap/Tab';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import InputGroup from 'react-bootstrap/InputGroup';
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown';
import FormControl from 'react-bootstrap/FormControl';

class AdminPage extends Component {
    constructor(props) {
        super(props);
        this.username = React.createRef();
        this.password = React.createRef();
        this.firstName = React.createRef();
        this.lastName = React.createRef();
        this.address = React.createRef();
        this.email = React.createRef();
        this.phone = React.createRef();
        this.bloodType = React.createRef();
        this.gender = React.createRef();
        this.age = React.createRef();

        this.state = {
            validated: false,
        };
    }

    handleSubmit = (event) => {
        const form = event.currentTarget;
        if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
        }

        this.setState({
            validated: true
        });
    };

    componentDidMount() {
    }

    async postDonor() {
        let username = this.username.current.value;
        let password = this.password.current.value;
    
        let donor = {
            FirstName: this.firstName.current.value,
            LastName: this.lastName.current.value,
            Address: this.address.current.value,
            Email: this.email.current.value,
            Phone: this.phone.current.value,
            BloodType: this.bloodType.current.value,
            Gender: this.gender.current.value,
            Age: this.age.current.value
        };
    
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(donor)
        };
    
        const response = await fetch(`https://localhost:44336/api/donor/username=${username}&password=${password}`, requestOptions);
        const data = await response.json();
    }

    cards = () => {
        let cardList = [];
        let cardHeaders = ["Donori", "Doniranje"]
        cardList.push(
            <Card className="admin-card" key="card1">
                <Card.Header className="admin-header" bg="success"><h5>{cardHeaders[0]}</h5></Card.Header>
                <Card.Body className="admin-card-body">
                    <Tabs
                        transition={false}
                        id="tabs"
                        className="mb-3"
                        defaultActiveKey="add"
                    >
                        <Tab eventKey="add" title="Dodaj" className="tab-css">
                            <Form noValidate validated={this.state.validated} onSubmit={this.handleSubmit} className="form-css">
                                <div className="form-column">
                                    <Form.Group className="input-css" controlId="validationCustom01">
                                        <Form.Label>Korisničko ime</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            placeholder="Korisničko ime"
                                            ref={this.username}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="validationCustom02">
                                        <Form.Label>Lozinka</Form.Label>
                                        <Form.Control
                                            required
                                            type="password"
                                            placeholder="Lozinka"
                                            ref={this.password}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                </div>
                                <div className="form-column">
                                    <Form.Group className="input-css" controlId="validationCustom03">
                                        <Form.Label>Ime</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            placeholder="Ime"
                                            ref={this.firstName}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="validationCustom04">
                                        <Form.Label>Prezime</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            placeholder="Prezime"
                                            ref={this.lastName}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                </div>
                                <div className="form-column">
                                    <Form.Group className="input-css" controlId="validationCustom05">
                                        <Form.Label>Dob</Form.Label>
                                        <Form.Control
                                            required
                                            type="number"
                                            placeholder="Dob"
                                            ref={this.age}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="gender">
                                        <Form.Label>Spol</Form.Label>
                                        <Form.Select ref={this.gender} className="gender-select">
                                            <option value="0">M</option>
                                            <option value="1">Ž</option>
                                            <option value="2">Drugo</option>
                                        </Form.Select>
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="gender">
                                        <Form.Label>Krvna grupa</Form.Label>
                                        <Form.Select ref={this.bloodType} className="gender-select">
                                            <option value="0-">0-</option>
                                            <option value="0+">0+</option>
                                            <option value="A-">A-</option>
                                            <option value="A+">A+</option>
                                            <option value="B-">B-</option>
                                            <option value="B+">B+</option>
                                            <option value="AB-">AB-</option>
                                            <option value="AB+">AB+</option>
                                        </Form.Select>
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                </div>
                                <div className="form-column">
                                    <Form.Group className="input-css" controlId="validationCustom06">
                                        <Form.Label>Adresa stanovanja</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            placeholder="Adresa"
                                            ref={this.address}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                </div>
                                <div className="form-column">
                                    <Form.Group className="input-css" controlId="validationCustom07">
                                        <Form.Label>E-mail</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            placeholder="E-mail"
                                            ref={this.email}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="validationCustom08">
                                        <Form.Label>Broj mobitela</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            placeholder="Mob"
                                            ref={this.phone}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                </div>
                                <Button variant="dark" onClick={this.postDonor()}>Dodaj</Button>
                            </Form>
                        </Tab>
                        <Tab eventKey="edit" title="Uredi">
                            <Form noValidate validated={this.state.validated} onSubmit={this.handleSubmit} className="form-css">
                                <div className="form-column">
                                    <InputGroup className="input-css username-drop">
                                        <DropdownButton
                                            variant="outline-secondary"
                                            title="Dropdown"
                                            id="input-group-dropdown-2"
                                            align="end"
                                        >
                                            <Dropdown.Item href="#">Action</Dropdown.Item>
                                            <Dropdown.Item href="#">Another action</Dropdown.Item>
                                            <Dropdown.Item href="#">Something else here</Dropdown.Item>
                                            <Dropdown.Divider />
                                            <Dropdown.Item href="#">Separated link</Dropdown.Item>
                                        </DropdownButton>
                                        <FormControl aria-label="Text input with dropdown button" />
                                        <Button variant="outline-secondary" id="button-addon2">
                                            🗘
                                        </Button>
                                    </InputGroup>

                                </div>
                                <Button variant="dark" type="submit">Dodaj</Button>
                            </Form>
                        </Tab>
                    </Tabs>
                </Card.Body>
            </Card>
        );
        cardList.push(
            <Card className="admin-card" key="card2">
                <Card.Header className="admin-header" bg="success"><h5>{cardHeaders[1]}</h5></Card.Header>
                <Card.Body className="admin-card-body">
                    <Card.Text>
                        here goes the inputs
                    </Card.Text>
                    <Button variant="dark">
                        Detalji
                    </Button>
                </Card.Body>
            </Card>
        );
        return cardList;
    }

    render() {
        return (
            <div className="admin">
                <div className="admin-body"></div>
                <div className="cards">
                    {this.cards()}
                </div>
            </div>
        );
    }
}

export default AdminPage;