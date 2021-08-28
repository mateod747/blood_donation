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

            // For post method (save)
            firstName: "",
            lastName: "",
            address: "",
            email: "",
            phone: "",
            bloodType: "0-",
            gender: "Male",
            age: 0,

            // For put method (edit)
            showStatusEdit: false,
            errorEdit: false,
            usernameEdit: "",
            firstNameEdit: "",
            lastNameEdit: "",
            addressEdit: "",
            emailEdit: "",
            phoneEdit: "",
            bloodTypeEdit: "0-",
            genderEdit: "Male",
            ageEdit: 0,
            donorId: "",

            // For Blood Transaction
            showStatusTransaction: false,
            transactionDonorId: "",
            transactionMessage: "",
            usernameTransaction: "",
            personnelGuid: "",

            //  For blood transaction editing
            editTransactionRadioAdd: true,
            editTransactionRadioEdit: false,
            editDay: 0,
            editMonth: 0,
            editYear: 0
        };
    }

    async getDonorData(event) {
        await fetch(`https://localhost:44336/api/donor?username=${this.state.usernameEdit}`,
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
                    firstNameEdit: json.firstName,
                    lastNameEdit: json.lastName,
                    addressEdit: json.address,
                    emailEdit: json.email,
                    phoneEdit: json.phone,
                    bloodTypeEdit: json.bloodType,
                    genderEdit: json.gender,
                    ageEdit: json.age,
                    donorId: json.donorID
                });
            })
            .catch((message) => {
                this.setState({
                    showStatus: true,
                    errorEdit: true
                })
            });
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

    async editDonor() {
        if (this.state.firstNameEdit !== "" &&
            this.state.lastNameEdit !== "" &&
            this.state.addressEdit !== "" &&
            this.state.emailEdit !== "" &&
            this.state.phoneEdit !== "" &&
            this.state.bloodTypeEdit !== "" &&
            this.state.ageEdit !== null) {

            let donorId = this.state.donorId;

            let donor = {
                DonorID: this.state.donorId,
                FirstName: this.state.firstNameEdit,
                LastName: this.state.lastNameEdit,
                Address: this.state.addressEdit,
                Email: this.state.emailEdit,
                Phone: this.state.phoneEdit,
                BloodType: this.state.bloodTypeEdit,
                Gender: this.state.genderEdit,
                Age: this.state.ageEdit
            };

            const requestOptions = {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    "Access-Control-Allow-Origin": "*",
                    "token": sessionStorage.getItem('loginToken')
                },
                body: JSON.stringify(donor)
            };

            const response = await fetch(`https://localhost:44336/api/donor`, requestOptions)
                .then(async res => res.json())
                .then(json => {
                    this.setState({
                        showStatusEdit: true,
                        errorEdit: false
                    });
                })
                .catch((message) => {
                    this.setState({
                        showStatusEdit: true,
                        errorEdit: true
                    });
                })
        }
    }

    async deleteDonor() {
        await this.editDonor();
        if (this.state.donorId !== "") {
            const requestOptions = {
                method: 'DELETE',
                headers: {
                    "Access-Control-Allow-Origin": "*",
                    "token": sessionStorage.getItem('loginToken')
                }
            };

            const response = await fetch(`https://localhost:44336/api/donor/${this.state.donorId}`, requestOptions)
                .then(async res => res.json())
                .then(json => {
                    this.setState({
                        showStatusEdit: true,
                        errorEdit: false
                    });
                })
                .catch((message) => {
                    this.setState({
                        showStatusEdit: true,
                        errorEdit: true
                    });
                })
        }
        else {
            this.setState({
                showStatusEdit: true,
                errorEdit: true
            });
        }
    }

    async doesDonorExist() {
        let donor = {
            Username: this.state.usernameTransaction,
            PersonnelId: this.state.personnelGuid
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

        await fetch(`https://localhost:44336/api/bloodtransaction`, requestOptions)
            .then(async res => await res.json())
            .then(json => {
                this.setState({
                    showStatusTransaction: true,
                    transactionDonorId: json.donorId,
                    transactionMessage: json.message,
                });
            })
            .catch((message) => {
                this.setState({
                    showStatusTransaction: true,
                    transactionMessage: "Error"
                })
            });
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
                        <Tab eventKey="edit" title="Uredi" className="tab-css">
                            <Form noValidate validated={this.state.validated} onSubmit={this.handleSubmit} className="form-css">
                                <div className="form-colum">
                                    <Form.Group className="input-css" controlId="validationCustom03345">
                                        <InputGroup className="username-drop">
                                            <FormControl
                                                type="text"
                                                placeholder="Korisničko ime"
                                                onChange={(event) => this.setState({
                                                    usernameEdit: event.target.value
                                                })}
                                            />
                                            <Button variant="outline-secondary" onClick={() => this.getDonorData()} id="button-addon2">
                                                🗘
                                            </Button>
                                            <Button variant="outline-secondary" onClick={() => this.deleteDonor()} id="button-addon2">
                                                ❌
                                            </Button>
                                        </InputGroup>
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
                                                firstNameEdit: event.target.value
                                            })}
                                            value={this.state.firstNameEdit}
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
                                                lastNameEdit: event.target.value
                                            })}
                                            value={this.state.lastNameEdit}
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
                                                ageEdit: Number(event.target.value)
                                            })}
                                            value={this.state.ageEdit}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="gender">
                                        <Form.Label>Spol</Form.Label>
                                        <Form.Select onChange={(event) => this.setState({
                                            genderEdit: event.target.value
                                        })}
                                            value={this.state.genderEdit}
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
                                            bloodTypeEdit: event.target.value
                                        })}
                                            value={this.state.bloodTypeEdit}
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
                                                addressEdit: event.target.value
                                            })}
                                            value={this.state.addressEdit}
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
                                                emailEdit: event.target.value
                                            })}
                                            value={this.state.emailEdit}
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
                                                phoneEdit: event.target.value
                                            })}
                                            value={this.state.phoneEdit}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                </div>
                                {this.state.showStatusEdit ? this.state.errorEdit === true ?
                                    <Alert variant="danger" onClose={() => this.setState({ showStatusEdit: false })} dismissible>
                                        <Alert.Heading>Pogrješka ili krivo korisničko ime!</Alert.Heading>
                                    </Alert> :
                                    <Alert variant="success" onClose={() => this.setState({ showStatusEdit: false })} dismissible>
                                        <Alert.Heading>Uspjeh!</Alert.Heading>
                                    </Alert> : null
                                }
                                <Button variant="dark" onClick={() => this.editDonor()}>Uredi</Button>
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
                    <Tabs
                        transition={false}
                        id="tabs"
                        className="mb-3"
                        defaultActiveKey="don"
                    >
                        <Tab eventKey="don" title="Donacije" className="tab-css">
                            <Form noValidate validated={this.state.validatedCard2} className="form-css" onSubmit={this.handleSubmit}>
                                <div className="form-column">
                                    <Form.Group className="check-input" controlId="formBasicCheckbox">
                                        <Form.Check
                                            isValid
                                            type="radio"
                                            label="Dodaj "
                                            onClick={(event) => {
                                                if (event.target.checked) {
                                                    this.setState({
                                                        editTransactionRadioAdd: true,
                                                        editTransactionRadioEdit: false
                                                    })
                                                }
                                            }}
                                            checked={this.state.editTransactionRadioAdd}
                                            variant="success" />
                                    </Form.Group>
                                    <Form.Group className="check-input" controlId="formBasicCheckbox">
                                        <Form.Check
                                            isValid
                                            type="radio"
                                            label="Uredi "
                                            onClick={(event) => {
                                                if (event.target.checked) {
                                                    this.setState({
                                                        editTransactionRadioAdd: false,
                                                        editTransactionRadioEdit: true
                                                    })
                                                }
                                            }
                                            }
                                            checked={this.state.editTransactionRadioEdit} />
                                    </Form.Group>
                                </div>
                                <div className="form-column" hidden={this.state.editTransactionRadioEdit}>
                                    <Form.Group className="input-css" controlId="validationCustom088">
                                        <Form.Label>Medicinsko osoblje</Form.Label>
                                        <Form.Control
                                            required
                                            type="text"
                                            placeholder="Medicinsko osoblje"
                                            onChange={(event) => this.setState({
                                                personnelGuid: event.target.value
                                            })}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                </div>
                                <div className="form-colum" hidden={this.state.editTransactionRadioAdd === true && this.state.editTransactionRadioEdit === true}>
                                    <Form.Group className="input-css">
                                        <InputGroup className="username-drop">
                                            <FormControl
                                                type="text"
                                                placeholder="Korisničko ime"
                                                onChange={(event) => this.setState({
                                                    usernameTransaction: event.target.value
                                                })}
                                            />
                                            <Button variant="outline-secondary" onClick={() => this.doesDonorExist()} id="button-addon2">
                                                {'->'}
                                            </Button>
                                        </InputGroup>
                                    </Form.Group>
                                    {this.state.showStatusTransaction ? this.state.transactionMessage != "" ?
                                        <Alert variant="danger" onClose={() => this.setState({ showStatusTransaction: false })} dismissible>
                                            <Alert.Heading>{this.state.transactionMessage}</Alert.Heading>
                                        </Alert> :
                                        <Alert variant="success" onClose={() => this.setState({ showStatusTransaction: false })} dismissible>
                                            <Alert.Heading>Točni podaci</Alert.Heading>
                                        </Alert> : null}
                                </div>
                                <div className="form-column" hidden={this.state.editTransactionRadioAdd}>
                                    <Form.Group className="input-css" controlId="validationCustom0111">
                                        <Form.Label>Dan</Form.Label>
                                        <Form.Control
                                            required
                                            type="number"
                                            placeholder="Dan"
                                            onChange={(event) => this.setState({
                                                editDay: Number(event.target.value)
                                            })}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="validationCustom01112">
                                        <Form.Label>Mjesec</Form.Label>
                                        <Form.Control
                                            required
                                            type="number"
                                            placeholder="Mjesec"
                                            onChange={(event) => this.setState({
                                                editMonth: Number(event.target.value)
                                            })}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Form.Group className="input-css" controlId="validationCustom01113">
                                        <Form.Label>Dan</Form.Label>
                                        <Form.Control
                                            required
                                            type="number"
                                            placeholder="Godina"
                                            onChange={(event) => this.setState({
                                                editYear: Number(event.target.value)
                                            })}
                                        />
                                        <Form.Control.Feedback>U redu</Form.Control.Feedback>
                                    </Form.Group>
                                    <Button variant="dark" onClick="" className="button-edit">Pretraži</Button>
                                </div>
                            </Form>
                        </Tab>
                        <Tab eventKey="per" title="Osoblje">


                        </Tab>
                        <Tab eventKey="rec" title="Primatelji">


                        </Tab>
                        <Tab eventKey="stock" title="Zaliha krvi">


                        </Tab>
                    </Tabs>
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