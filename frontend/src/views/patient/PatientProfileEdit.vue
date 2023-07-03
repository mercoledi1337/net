<template>
    <section id="edit-profile">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <breadcrumbs is-patient>
                        <router-link to="/profil">Mój profil</router-link>
                        <router-link to="/profil/edycja-profilu">Edycja profilu</router-link>
                    </breadcrumbs>
                    <hello-message :name="user.firstName" icon-name="clipboard"><template v-slot:info>Tutaj możesz edytować swoje dane personalne oraz dodać przyjmowane leki</template></hello-message>
                    <div class="d-flex flex-column align-items-center"> 
                        <div class="col-md-6">
                            <h1>Edycja profilu</h1>
                            <form @submit.prevent="submitForm">
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="name">Imię</label>
                                    <input type="text" class="form-control" id="name" v-model.trim="patient.user.firstName">
                                </div>
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="surname">Nazwisko</label>
                                    <input type="text" class="form-control" id="surname" v-model.trim="patient.user.lastName">
                                </div>
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="email">Adres email</label>
                                    <input type="email" class="form-control" id="email" v-model.trim="patient.user.emailAddress">
                                </div>
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="tel">Telefon kontaktowy</label>
                                    <input type="tel" class="form-control" id="tel" v-model.trim="patient.phoneNumber">
                                </div>
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="pesel">PESEL</label>
                                    <input type="text" class="form-control" id="pesel" v-model.trim="patient.pesel">
                                </div>
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="city">Miejscowość</label>
                                    <input type="text" class="form-control" id="city"  v-model.trim="patient.city">
                                </div>
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="postal-code">Kod pocztowy</label>
                                    <input type="text" class="form-control" pattern="[0-9]{2}-[0-9]{3}" placeholder="00-000" v-model.trim="patient.postalCode" id="postal-code">
                                </div>
                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="street">Ulica i nr budynku</label>
                                    <input type="text" class="form-control" id="street" v-model.trim="patient.streetAddress">
                                </div>         

                                <div class="form-group d-flex flex-column">
                                    <label class="align-self-start" for="allergies">Alergie</label>
                                    <textarea id="allergies" rows="3" v-model.trim="patient.allergies"></textarea>
                                </div>
                                
                                <base-button type="dark">Zapisz zmiany</base-button>
                                <router-link to="/profil"><base-button type="light">Anuluj</base-button></router-link>
                            </form>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<script>

import { mapGetters } from 'vuex';
import axios from 'axios'

export default {
    computed: {
        ...mapGetters(['patient', 'user'])
    },
    async created(){
        // const tokenDecoded = localStorage.decodedToken;
        // const getPatientInfo = await axios.get(`Patients/${tokenDecoded.nameid}`);
        
        // await this.$store.dispatch('patient', getPatientInfo.data.data);
    },

    methods: {
        submitForm(){
            axios
            .put(`Patients`, {
                id: this.patient.id,
                user: {
                    id: this.patient.id,
                    firstName: this.patient.user.firstName,
                    lastName: this.patient.user.lastName,
                    emailAddress: this.patient.user.emailAddress,
                    accountType: "Patient",
                },
                pesel: this.patient.pesel,
                phoneNumber: this.patient.phoneNumber,
                allergies: this.patient.allergies,
                city: this.patient.city,
                postalCode: this.patient.postalCode,
                streetAddress: this.patient.street
            })
            .then(() => {
                this.$router.replace('/profil');
            })
            .catch((error) => {
                console.log(error)
            })
        },  
    }
}

</script>

<style lang="scss" scoped>
.col-md-6 {
        form {
            .form-group {
                margin: 13px 0;

                label {
                    font-weight: 500;
                    font-size: 14px;
                    line-height: 30px;
                    letter-spacing: -0.001em;
                    color: $secondary;

                }
                input {
                    box-sizing: border-box;
                    border: 1px solid #5F6D7E;
                    border-radius: 8px;
                    display: flex;
                    padding: 8px 18px;
                    gap: 16px;
                    background: #F8F9FB;

                }

                textarea {
                    box-sizing: border-box;
                    border: 1px solid #5F6D7E;
                    border-radius: 8px;
                    background: #F8F9FB;
                    padding: 8px 18px;
                    resize: none;
                    
                }
            }

            .form-check {
                input {
                    border-color: $secondary;
                    &:checked {
                        background-color: $teal;
                        border-color: $secondary;
                    }
                }

                label {
                    margin-left: 10px;
                    font-weight: 500;
                    color: $secondary;

                    span {

                        a {
                            color: $teal;
                        }
                        
                    }
                } 
            }

            p {
                color: $button-dark;
                font-style: normal;
                font-weight: 600;
                font-size: 15px;
                line-height: 22px;
                letter-spacing: -0.001em;
                color: $secondary;
            
                a {
                    color: $teal;
                }
        
            } 
        }
        h1 {
            margin: 60px 0;
            font-weight: 700;
            font-weight: 700;
            font-size: 52px;
            line-height: 60px;
            text-align: center;
            letter-spacing: -0.01em;
            color: $button-dark;
        }
    }
    .base-button {
        margin: 30px 10px;

        .span-icon {
            display: inline-block;
            transition: all 0.2s ease-in-out;
        }
    }

    .base-button:hover .span-icon {
        transform: translateX(5px);
    }

</style>