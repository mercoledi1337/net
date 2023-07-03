<template>
    <section id="patient-appointment-app">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <breadcrumbs is-patient>
                        <router-link to="/umow-wizyte">Umów wizytę</router-link>
                    </breadcrumbs>
                    <hello-message v-if="user" :name="user.firstName" icon-name="appointment"><template v-slot:info>Zarezerwuj wizytę do specjalisty</template></hello-message>
                    <div class="appointment-container">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="left-outer-wrapper d-flex align-items-center flex-column">
                                    <div class="left-inner-wrapper d-flex flex-column align-items-center">
                                        <div class="form-group d-flex flex-column">
                                            <label class="align-self-start" for="specialization">Wybierz specjalizację</label>
                                            <select class="form-select" aria-label="Wybierz specjalistę" id="specialization" @change="onSpecializationChange($event)" v-model="specialization">
                                                <option selected disabled hidden>Nie wybrano</option>
                                                <option v-for="specialization in specialization_data" :key="specialization" :value="specialization">
                                                    {{ specialization }}
                                                </option>
                                            </select>
                                        </div>
                                        <div class="form-group d-flex flex-column">
                                            <label class="align-self-start" for="doctor">Wybierz lekarza</label>
                                            <select class="form-select" aria-label="Wybierz specjalistę" id="doctor" v-model="doctor" disabled v-if="specialization == 'Nie wybrano'">
                                                <option selected disabled hidden>Nie wybrano</option>
                                            </select>
                                            <select class="form-select" aria-label="Wybierz specjalistę" id="doctor" v-model="doctor" @change="getAvailableDates()" v-else>
                                                <option selected disabled hidden>Nie wybrano</option>
                                                <option v-for="doctor in doctor_data" :key=doctor.id v-bind:value="{ id: doctor.id, firstname: doctor.user.firstName, lastname: doctor.user.lastName }">
                                                    {{ doctor.user.firstName }} {{ doctor.user.lastName }}
                                                </option>
                                            </select>
                                        </div>

                                        <div class="appointment-date-picker d-flex flex-column">
                                            <div class="form-group d-flex flex-column">
                                                <label class="align-self-start">Wybierz termin</label>
                                                <div class="date-picker d-flex justify-content-evenly">
                                                    <svg @click="previousDay()" width="30" height="31" viewBox="0 0 30 31" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <rect width="30" height="30" rx="15" transform="matrix(-1 0 0 1 30 0.5)" fill="white"/>
                                                        <rect width="30" height="30" rx="15" transform="matrix(-1 0 0 1 30 0.5)" fill="black" fill-opacity="0.2"/>
                                                        <path d="M17.5 8L10.8839 14.6161C10.3957 15.1043 10.3957 15.8957 10.8839 16.3839L17.5 23" stroke="#5F6D7E" stroke-width="1.5" stroke-linecap="round"/>
                                                    </svg>
                                                    <VueDatePicker v-model="date" @update:model-value="getAvailableDates()" input-class-name="datepicker-input" :format="format" locale="pl" :disabled-week-days="[6, 0]" :enable-time-picker="false" select-text="Wybierz" cancel-text="Anuluj" hide-input-icon auto-apply :min-date="new Date()" :clearable="false">{{ date }}</VueDatePicker>
                                                    <svg @click="nextDay()" width="30" height="31" viewBox="0 0 30 31" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <rect y="0.5" width="30" height="30" rx="15" fill="white"/>
                                                        <rect y="0.5" width="30" height="30" rx="15" fill="black" fill-opacity="0.2"/>
                                                        <path d="M12.5 8L19.1161 14.6161C19.6043 15.1043 19.6043 15.8957 19.1161 16.3839L12.5 23" stroke="#5F6D7E" stroke-width="1.5" stroke-linecap="round"/>
                                                    </svg>
                                                </div>
                                            </div>
                                            <div class="appointment-dates d-flex flex-column">
                                                <div v-if="doctor == 'Nie wybrano'" style="filter: blur(3px)" class="d-flex flex-column">
                                                    <base-button type='inaccessible' style="text-decoration: none;">09:00 - 09:30</base-button>
                                                    <base-button type='inaccessible' style="text-decoration: none;">09:30 - 10:00</base-button>
                                                    <base-button type='inaccessible' style="text-decoration: none;">10:00 - 10:30</base-button>
                                                    <base-button type='inaccessible' style="text-decoration: none;">10:30 - 11:00</base-button>
                                                    <!-- <a class="dates-button">Pokaż więcej godzin</a>      -->
                                                </div>
                                                <template v-else>
                                                    <template v-if="showLess">
                                                    <base-button v-for="date in available_dates.slice(0, 4)" @click="selectDate({ id: date.id, starttime: date.startTime, endtime: date.endTime, status: date.status })" :class="{ 'active': date.id === this.slot_id }" :key="date.id" :type="date.status ? 'light' : 'inaccessible'">{{ date.startTime.slice(0, 5) }} - {{ date.endTime.slice(0, 5) }}</base-button>
                                                    <a @click="showLess=false" class="dates-button">Pokaż więcej godzin</a>
                                                    </template>
                                                    <template v-else>
                                                        <base-button v-for="date in available_dates" @click="selectDate({ id: date.id, starttime: date.startTime, endtime: date.endTime, status: date.status })" :class="{ 'active': date.id === this.slot_id }" :key="date.id" :type="date.status ? 'light' : 'inaccessible'">{{ date.startTime.slice(0, 5) }} - {{ date.endTime.slice(0, 5) }}</base-button>
                                                        <a @click="showLess=true" class="dates-button">Pokaż mniej godzin</a>
                                                    </template>
                                                </template>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="right-wrapper d-flex flex-column">
                                    <div class="right-inner-wrapper d-flex flex-column align-items-center">
                                        <img src="@/assets/images/icons/doctor.png">
                                        <p>Twój lekarz:</p>
                                        <p v-if="doctor != 'Nie wybrano'"> {{ doctor.firstname }} {{ doctor.lastname }}</p>
                                        <p v-if="specialization != 'Nie wybrano'"> {{ specialization }}</p>
                                        <p v-if="doctor != 'Nie wybrano'"><span>Termin:</span> {{ date.toLocaleDateString('pl', { weekday:"long", year:"numeric", month:"long", day:"numeric"}) }}<template v-if="slot != null">, {{ slot.starttime.slice(0, 5) }}</template></p>
                                        <p v-else><span>Termin: </span> Wybierz termin</p>
                                        <div class="d-flex">
                                            <p>Koszt wizyty:</p>
                                            <span> {{ price }} zł</span>
                                        </div>
                                        <base-button @click="submitForm()" type="dark" :has-icon="true">Umów wizytę</base-button>
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
import axios from 'axios'
// import jwt_decode from "jwt-decode";
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';
import { useToast } from "vue-toastification";

export default {
    components: { VueDatePicker },
    setup() {
        const toast = useToast();
        return { toast }
    },

    data(){
        return {
            doctor: "Nie wybrano",
            specialization: "Nie wybrano",
            termin: 'Nie wybrano',
            showLess: true,
            price: 100,
            specialization_data: [],
            doctor_data: [],
            available_dates: [],
            date: new Date(),//.toLocaleDateString('pl', { weekday:"long", year:"numeric", month:"short", day:"numeric"}),
            slot: null,
            slot_id: -1
        }
    },
    methods: {
        async onSpecializationChange(event) {
            await axios.get('https://localhost:7121/api/Uzytkownik/lekarze' + event.target.value).
                then(response => (this.doctor_data = response.data));
        },

        async getAvailableDates() {
            if(this.doctor != "Nie wybrano") {
                this.slot_id = -1;
                this.slot = null;
                const parameters = { doctorId: this.doctor.id, date: this.date.toLocaleDateString("sv") };
                await axios.post('Visits/Slots', parameters).
                    then(response => (this.available_dates = response.data.data));
            } 
        },

        async submitForm(){
            if(this.specialization == 'Nie wybrano') {
                this.toast.warning("Nie wybrano specjalizacji", {
                    timeout: 2500,
                    position: "bottom-right",
                });
            }
            else if(this.doctor == 'Nie wybrano') {
                this.toast.warning("Nie wybrano doktora", {
                    timeout: 2500,
                    position: "bottom-right",
                });
            }
            else if(this.slot_id == -1) {
                this.toast.warning("Nie wybrano godziny", {
                    timeout: 2500,
                    position: "bottom-right",
                });
            }
            else {
                const response = await axios.post('Visits', {
                    patientId: 1,
                    doctorId: this.doctor.id,
                    date: this.date.toLocaleDateString("sv"),
                    slotId: this.slot.id,
                });

                if(response.status == 200) {
                    this.toast.success("Wizyta umówiona", {
                        timeout: 2500,
                        position: "bottom-right",
                    });
                    this.$router.replace({name: "patient-visits"});
                }
            }

        },

        nextDay() {
            if(this.date.getDay() == 5) {
                this.date = new Date(this.date.setDate(this.date.getDate()+3));
            }
            else {
                this.date = new Date(this.date.setDate(this.date.getDate()+1));
            }
            this.getAvailableDates();
        },

        previousDay() {
            if(!(this.date < new Date())) {
                if(this.date.getDay() == 1) {
                    this.date = new Date(this.date.setDate(this.date.getDate()-3));
                }
                else {
                    this.date = new Date(this.date.setDate(this.date.getDate()-1));
                }
            }
            this.getAvailableDates();
        },

        selectDate(object) {
            if(object.status) {
                this.slot = object;
                this.slot_id = object.id;
            }
        },

        format() {
            return this.date.toLocaleDateString('pl', { weekday:"long", year:"numeric", month:"short", day:"numeric"});
        }
    },

    computed: {
        ...mapGetters(['user'])
    },

    async created(){
        // const token = localStorage.getItem('token');
        // const token_decoded = jwt_decode(token);

        // const response = await axios.get(`Patients`);
        // //const response2 = await axios.get(`User/${token_decoded.nameid}`);

        // await this.$store.dispatch('patient', response.data.data[token_decoded.nameid-1])
        // await this.$store.dispatch('user', response.data.data[token_decoded.nameid-1].user);
    },

    async mounted(){
        await axios.get('Doctors/Specializations').
            then(response => (this.specialization_data = response.data.data));
    },
}
</script>

<style lang="scss" scoped>
div.appointment-container {
    margin: 40px 0;
    padding: 50px 0;
    background-color: $primary;
    border-radius: 5px;
    border: 1px solid $button-light;

    div.left-outer-wrapper {
        width: 100%;
        border-right: 1px solid $button-light;
        margin-bottom: 40px;

        @media (max-width: 992px) { 
            border-right: none;
        }

        div.left-inner-wrapper {
            @media (max-width: 992px) { 
                width: 80%;
                padding: 0 40px 40px;
                border-bottom: 1px solid $button-light;
            }
        
            div.form-group {
                margin: 13px 0;
                width: 300px;

                label {
                    font-weight: 500;
                    font-size: 14px;
                    line-height: 30px;
                    letter-spacing: -0.001em;
                    color: $secondary;
                    margin-bottom: 10px;

                }
                select {
                    box-sizing: border-box;
                    border: 1px solid #5F6D7E;
                    border-radius: 8px;
                    display: flex;
                    padding: 8px 18px;
                    gap: 16px;
                    background-color: #F8F9FB;
                }
            }
        
            div.appointment-date-picker {
                div.date-picker {
                    svg {
                        &:hover {
                            cursor: pointer;
                        }
                    }
                    span {
                        width: 160px;
                        line-height: 31px;
                    }
                }
                .base-button {
                    margin: 10px;
                }
            }
            .base-button {
                margin: 10px;
            }

                a {
                    margin-top: 18px;
                    color: $secondary;
                    font-weight: 600;
                    font-size: 15px;
                }

        }

        .appointment-dates {
            height: 305px;
            overflow-y: auto;
            .dates-button {
                text-decoration: underline;
                cursor: pointer;
                user-select: none;
            }

            &::-webkit-scrollbar {
                width: 7px;
                height: 7px;
                left:-100px;
            }

            &::-webkit-scrollbar-track {
                background-color: white;
                margin-left: 5px;
            }

            &::-webkit-scrollbar-thumb {
                background-color: #5f6d7e;
                border-radius: 8px;
            }

            &::-webkit-scrollbar-thumb:hover {
                background-color: rgba(12, 229, 207, 1);
            }

            button.active {
                background-color: rgb(48, 54, 69);
                border-color: rgb(48, 54, 69);
                color: white;
            }
        }
        .form-check {
            input {
                border-color: $secondary;
                &:checked {
                    background-color: $teal;
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

    div.right-wrapper {
        height: 100%;
        justify-content: center;
        div.right-inner-wrapper{
            p:nth-child(2){
                text-transform: uppercase;
                color: $secondary;
                margin: 30px 0;
            }
            
            p:nth-child(5){
                margin-bottom: 30px;
            }

            img {
                width: 90px;
                user-select: none;
            }

            p {
                span {
                    font-weight: 600;
                }
                user-select: none;
            }

            div {
                width: 260px;
                border-top: 1px solid #D1D9E2;
                padding: 20px 0;
                justify-content: space-evenly;
                align-items: center;
                font-weight: 700;

                p {
                    margin-bottom: 0;
                }

                span {
                    background-color: $secondary;
                    padding: 2px 8px;
                    border-radius: 5px;
                    color: white;
                }
            }
        }
    }
}
</style>

<style>
    .dp__theme_light {
    --dp-background-color: #f8f9fb;
    --dp-text-color: #2C3E50;
    --dp-hover-color: #f3f3f3;
    --dp-hover-text-color: #2C3E50;
    --dp-hover-icon-color: #959595;
    --dp-primary-color: rgb(105, 226, 207);
    --dp-primary-text-color: #f8f5f5;
    --dp-secondary-color: #c0c4cc;
    --dp-border-color: #f8f9fb;
    --dp-menu-border-color: #ddd;
    --dp-border-color-hover: #f8f9fb;
    --dp-disabled-color: #f6f6f6;
    --dp-scroll-bar-background: #f3f3f3;
    --dp-scroll-bar-color: #959595;
    --dp-success-color: #76d275;
    --dp-success-color-disabled: #a3d9b1;
    --dp-icon-color: #959595;
    --dp-danger-color: #ff6f60;
    --dp-highlight-color: rgba(25, 118, 210, 0.1);
    --dp-font-family: 'Inter', sans-serif;
    }

    .datepicker-input {
        text-align: center!important;
        line-height: 31px!important;
        padding: 0!important;
        align-items: center;
    }
</style>