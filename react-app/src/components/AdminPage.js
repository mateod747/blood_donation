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
import Alert from 'react-bootstrap/Alert';

class AdminPage extends Component {
    constructor(props) {
        super(props);
        this.state = {
            showStatus: false,
            error: false,
            username: "",
            password: "",
            firstName: "",
            lastName: "",
            address: "",
            email: "",
            phone: "",
            bloodType: "0-",
            gender: "Male",
            age: 0,
        };
    }

    componentDidMount() {
    }

    async postDonor() {
        if (this.state.firstName !== "" &&
            this.state.lastName !== "" &&
            this.state.address !== "" &&
            this.state.email !== "" &&
            this.state.phone !== "" &&
            this.state.bloodType !== "" &&
            this.state.age !== null) {

            let username = this.state.username;
            let password = this.state.password;

            let donor = {
                FirstName: this.state.firstName,
                LastName: this.state.lastName,
                Address: this.state.address,
                Email: this.state.email,
                Phone: this.state.phone,
                BloodType: this.state.bloodType,
                Gender: this.state.gender,
                Age: this.state.age
            };

            const requestOptions = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    "Access-Control-Allow-Origin": "*",
                    "token": sessionStorage.getItem('loginToken')
                },
                body: JSON.stringify(donor)
            };

            const response = await fetch(`https://localhost:44336/api/donor?username=${username}&password=${password}`, requestOptions)
                .then(async res => res.json())
                .then(json => {
                    this.setState({
                        showStatus: true,
                        error: false
                    });
                })
                .catch((message) => {
                    this.setState({
                        showStatus: true,
                        error: true
                    });
                })
        }
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
                            <Form noValidate className="form-css">
                                <div className="form-column">
                                    <Form.Group className="input-css" controlId="validationCustom01">
                                        <Form.Label>Korisničko ime</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            placeholder="Korisničko ime"
                                            onChange={(event) => this.setState({
                                                username: event.target.value
                                            })}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="validationCustom02">
                                        <Form.Label>Lozinka</Form.Label>
                                        <Form.Control
                                            required
                                            type="password"
                                            placeholder="Lozinka"
                                            onChange={(event) => this.setState({
                                                password: event.target.value
                                            })}
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
                                            onChange={(event) => this.setState({
                                                firstName: event.target.value
                                            })}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="validationCustom04">
                                        <Form.Label>Prezime</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            placeholder="Prezime"
                                            onChange={(event) => this.setState({
                                                lastName: event.target.value
                                            })}
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
                                            onChange={(event) => this.setState({
                                                age: Number(event.target.value)
                                            })}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="gender">
                                        <Form.Label>Spol</Form.Label>
                                        <Form.Select onChange={(event) => this.setState({
                                            gender: event.target.value
                                        })}
                                            className="gender-select">
                                            <option value={"Male"}>M</option>
                                            <option value={"Female"}>Ž</option>
                                            <option value={"Other"}>Drugo</option>
                                        </Form.Select>
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="gender">
                                        <Form.Label>Krvna grupa</Form.Label>
                                        <Form.Select onChange={(event) => this.setState({
                                            bloodType: event.target.value
                                        })}
                                            className="gender-select">
                                            <option value={"0-"}>0-</option>
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
                                            onChange={(event) => this.setState({
                                                address: event.target.value
                                            })}
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
                                            onChange={(event) => this.setState({
                                                email: event.target.value
                                            })}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="validationCustom08">
                                        <Form.Label>Broj mobitela</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            placeholder="Mob"
                                            onChange={(event) => this.setState({
                                                phone: event.target.value
                                            })}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                </div>
                                {this.state.showStatus ? this.state.error === true ?
                                    <Alert variant="danger" onClose={() => this.setState({ showStatus: false })} dismissible>
                                        <Alert.Heading>Pogrješka!</Alert.Heading>
                                    </Alert> :
                                    <Alert variant="success" onClose={() => this.setState({ showStatus: false })} dismissible>
                                        <Alert.Heading>Uspjeh!</Alert.Heading>
                                    </Alert> : null
                                }
                                <Button variant="dark" onClick={() => this.postDonor()}>Dodaj</Button>
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