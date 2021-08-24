
import React, { Component } from 'react';
import '../App.scss';
import '../layouts/DonationList.css';
import Pagination from 'react-bootstrap/Pagination';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import success from '../public/success.svg';
import fail from '../public/fail.svg';
import Modal from 'react-bootstrap/Modal';

let items = [];

function MyVerticallyCenteredModal(props) {
    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            dialogClassName="donation-modal"
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Detalji
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <h4><u>Zaposlenik: {props.donation.personnelName}</u></h4>
                <p> <br></br> Mob: {props.donation.personnelWorkPhone} <br></br> </p>
                <h4><u>Podaci s doniranja:</u> <br></br></h4>
                <p> Razina hemoglobina: {props.donation.hemoglobin} g/L <br></br></p>
                <p> Krvni tlak: {props.donation.bloodPressure} mmHg <br></br></p>
                {props.donation.notes !== "" ? <><h5><u>Komentar:</u></h5> <p> {props.donation.notes} <br></br></p></> : null}

                {
                    props.donation.recipientName !== "" ?
                        <div>
                            {props.donation.anon === 0 ?
                                <p>
                                    Vaša krv je darovana {props.donation.dateOut} 😀 <br></br>
                                    Primatelj: {props.donation.recipientName} <br></br>
                                    Krvna grupa: {props.donation.recipientBloodType}
                                </p> : null
                            }
                            {props.donation.anon === 1 ?
                                <p>
                                    Vaša krv je darovana anonimno. <br></br>
                                </p> : null
                            }
                            {props.donation.anon === 2 ?
                                <p>
                                    Vaša krv je darovana u znanstvene svrhe {props.donation.dateOut} 😀 <br></br>
                                    Primatelj: {props.donation.recipientName} <br></br>
                                </p> : null
                            }
                        </div>
                        : props.donation.recipientName !== null ? null : <p>Krv čeka primatelja</p>
                }
            </Modal.Body>
            <Modal.Footer>
                <Button variant="danger" onClick={props.onHide}>Zatvori</Button>
            </Modal.Footer>
        </Modal>
    );
}

class DonationList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            isLoaded: false,
            currentPage: [],
            paginationItems: [],
            active: 1,
            modalShow: false,
            modalDonation: ''
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
            });
    }

    async paginationClicked(event) {
        await this.fetchDonationList(event.target.id);

        this.setState({
            active: Number(event.target.id)
        });

        items = [];

        for (let number = 1; number <= Math.ceil(this.state.currentPage.listCount / 3); number++) {
            items.push(
                <Pagination.Item id={number} onClick={(event) => this.paginationClicked(event)} href={''} key={number} active={number === this.state.active}>
                    {number}
                </Pagination.Item>
            );
        };

        this.setState({
            paginationItems: items
        });
    };

    async componentDidMount() {
        await this.fetchDonationList(1);

        if (this.state.isLoaded) {
            for (let number = 1; number <= Math.ceil(this.state.currentPage.listCount / 3); number++) {
                items.push(
                    <Pagination.Item id={number} onClick={(event) => this.paginationClicked(event)} href={''} key={number} active={number === this.state.active}>
                        {number}
                    </Pagination.Item>
                );
            };
        }
        this.setState({
            paginationItems: items
        });
    };



    render() {
        if (!this.state.isLoaded) {
            return (null);
        }
        return (
            <>
                <div className="donation">
                    <div className="donation-body">
                    </div>
                    <div className="cards">
                        {this.state.currentPage.donations.map(item => {
                            return (
                                <Card className="donation-card" key={item.dateDonated}>
                                    <Card.Header className="donation-header-success" bg="success"><h5>{item.dateDonated}</h5></Card.Header>
                                    <Card.Body className="donation-card-body">
                                        <Card.Title>{item.quantity > 0 ? "Količina: " + item.quantity + "ml" : ":("}</Card.Title>
                                        <Card.Text>
                                            {item.notes !== "" ? item.notes : item.success ? "Donacija uspješna" : "Donacija neuspješna"}
                                        </Card.Text>
                                        <img src={item.success ? success : fail} className={item.success ? "status-check success-check" : "status-check fail-check"} alt="" />
                                        <Button variant="dark" onClick={() => this.setState({
                                            modalShow: true,
                                            modalDonation: item
                                        })}>
                                            Detalji
                                        </Button>
                                    </Card.Body>
                                </Card>
                            )
                        })}
                    </div>
                    <div className="pagination-css">
                        <Pagination size="lg">{this.state.paginationItems}</Pagination>
                    </div>
                </div>

                <MyVerticallyCenteredModal
                    show={this.state.modalShow}
                    donation={this.state.modalDonation}
                    onHide={() => this.setState({ modalShow: false })}
                />
            </>
        );
    }
}

export default DonationList;