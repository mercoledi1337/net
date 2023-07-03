<template>
    <section id="profile">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <breadcrumbs is-patient>
                        <router-link to="/profil">Mój profil</router-link>
                    </breadcrumbs>
                    <hello-message :name="user.firstName" icon-name="clipboard">
                        <template v-slot:info>Tutaj możesz zobaczyć swój profil pacjenta</template>
                    </hello-message>
                    <div class="d-flex flex-column align-items-center"> 
                        <div class="col-md-12">  
                            <div class="wrapper d-flex flex-column">
                                <div class="name d-flex flex-column align-items-center">
                                    <img src="@/assets/images/icons/svg/profile.svg">
                                    <span v-if="patient"> {{ capitalizeFirstLetter(patient.user.firstName)  }} {{ capitalizeFirstLetter(patient.user.lastName)}}</span>
                                </div>
                                <div class="buttons">
                                    <router-link to="/profil/edycja-profilu"><base-button type="dark">Edytuj profil</base-button></router-link>
                                    <router-link to="/profil/edycja-hasla"><base-button type="dark">Zmień hasło</base-button></router-link>
                                </div>
                            </div>
                            <div class="row base-cards-outer-wrapper">
                                <div class="col-lg-6">
                                    <base-card class="base-card" @click="toggleModalIsOpen">
                                        <template v-slot:title>Dane personalne</template>
                                        <template  v-slot:content>
                                            <p><span>Adres email: </span> {{ patient.user.emailAddress }}</p>
                                            <p><span>Telefon kontaktowy: </span> {{ patient.phoneNumber}} </p>
                                            <p><span>PESEL: </span> {{ patient.pesel }} </p>
                                            <p><span>Miejscowość: </span> {{ patient.city }} </p>
                                            <p><span>Kod pocztowy: </span> {{ patient.postalCode}}</p>
                                            <p><span>Ulica i nr budynku: </span> {{ patient.streetAddress }}</p>
                                        </template>
                                    </base-card>
                                </div>
                                <div class="col-lg-6">
                                    <div class="base-cards-inner-wrapper d-flex flex-column justify-content-between">
                                        <base-card  class="base-card-sm" @click="toggleModalIsOpen">
                                            <template v-slot:title>Przyjmowane leki</template>
                                            <template v-slot:content>
                                                <p>Lorem ipsum</p>
                                            </template>
                                            <template v-slot:button>Pokaż więcej</template>
                                        </base-card>
                                        <base-card class="base-card-sm" @click="toggleModalIsOpen">
                                            <template v-slot:title>Alergie</template>
                                            <template v-slot:content>
                                                <p v-if="patient.allergies"> {{ patient.allergies }}</p>
                                                <p v-else>Brak alergii (możesz je dodać edytując profil)</p>
                                            </template>
                                            <template v-slot:button>Pokaż więcej</template>
                                        </base-card>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
    </section>
</template>

<script>
import { mapGetters } from 'vuex';
import jwt_decode from "jwt-decode";
import axios from 'axios'

export default {
    computed: {
        ...mapGetters(['user', 'patient'])
    },
    async created(){
        const token = localStorage.getItem('token');
        const tokenDecoded = jwt_decode(token);

        const getUserInfo = await axios.get(`https://localhost:7121/api/Uzytkownik/Dane-personalne/${tokenDecoded.nameid}`); // wymagane do działania nawigacji
        const getPatientInfo = await axios.get(`https://localhost:7121/api/Uzytkownik/Dane-personalne/${tokenDecoded.nameid}`);
        await this.$store.dispatch('user', getUserInfo.data);
        await this.$store.dispatch('patient', getPatientInfo.data);
        console.log(getPatientInfo);
    },
} 
</script>

<style lang="scss" scoped>
#profile {
    @media (min-width: 992px) {
        padding-bottom: 40px;
    }
}

div.wrapper {

    div.name {
        img {
            width: 90px;
        }
        span {
            font-weight: 700;
            font-size: 32px;
            line-height: 40px;
            letter-spacing: -0.02em;
            margin-top: 20px;
        }
    }
    div.buttons {
        display: flex;
        flex-direction: column;
        align-self: flex-end;
        margin: 24px 0;

        @media (max-width: 992px) { 
            flex-direction: row;
            align-self: center;
        }
        button {
            margin: 12px;
        }
    }
}

div.base-cards-outer-wrapper {
    margin-top: 20px;
        .base-card > div:nth-child(1) > div:nth-child(2) > div:nth-child(2)  {
            p {
                margin-bottom: 12px;
            }
        } 

        .base-cards-inner-wrapper {
            height: 100%;
        }
}

@media (max-width: 992px) { 
    .col-lg-6, .base-cards-inner-wrapper {
    
        div.base-card, .base-card-sm {
            margin: 20px 0;
            
        }
    }
}


</style>