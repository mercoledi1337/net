<template>
    <div>
        <div @click="$emit('closePatientCardModal')" class="overlay"></div>
        <dialog open>
            <div class="wrapper d-flex flex-column align-items-center">
                <img @click="$emit('closePatientCardModal')" class="close-button" src="@/assets/images/icons/svg/base_modal_close.svg">
                <img class="profile-icon" src="@/assets/images/icons/svg/profile.svg">
                <p> {{ data.user.firstName }} {{ data.user.lastName}}</p>
                <div class="patient-info">
                    <p><span>Telefon komórkowy:</span> {{ data.phoneNumber}}</p>
                    <p><span>PESEL:</span> {{ data.pesel }}</p>
                    <p><span>Adres zamieszkania:</span> {{ data.streetAddress }} {{ data.postalCode}} {{ data.city}}</p>
                    <p><span>Przyjmowane leki:</span> lorem ipsum, lorem ipsum, lorem ipsum</p>
                    <p v-if="data.allergies"><span>Alergie:</span> {{ data.allergies }}</p>
                    <p v-else><span>Alergie:</span> brak </p>
                </div>
            </div>
        </dialog>    
    </div>
</template>

<script>
export default {
    name: "PatientCardModal",
    emits: ["closePatientCardModal"],
    props: {
        data: {
            type: Object,
            default: function() {
                return {}
            },
        },
    },
}
</script>

<style lang="scss" scoped>
    div {
        div.overlay {
            position: fixed;
            top: 0;
            left: 0;
            height: 100vh;
            width: 100%;
            z-index: 0;
            background-color: rgba(0, 0, 0, 0.35);
        }
        
        dialog {
            border: 0;
            padding: 0;
            position: fixed;
            left: 50%;
            margin: 0;
            top: 50%;
            transform: translate(-50%, -50%);
            background-color: rgba(0, 0, 0, 0);

            div.wrapper {
                border: 1.5px solid $secondary;
                background-color: $base-modal-background;
                border-radius: 10px;
                filter: drop-shadow(0 0 30px #999);
                padding: 50px;
                position: relative;
                max-width: 600px;

                img {
                    &.profile-icon {
                        width: 70px;
                    }
                    &.close-button {
                        width: 30px;
                        position: absolute;
                        right: 30px;
                        top: 30px;
                        cursor: pointer;
                    }
                }

                p {
                    font-weight: 700;
                    font-size: 32px;
                    line-height: 40px;
                    text-align: center;
                    letter-spacing: -0.02em;
                    color: #2E3646;
                    margin-top: 24px;
                }

                div.patient-info {
                    margin-top: 0px;

                    p {
                        text-align: left;
                        font-size: 20px;
                        color: $secondary;
                        font-weight: 400;
                        margin: 0;
                        span {
                            font-weight: 600;
                        }
                    }
                }
            }
        }
    }
</style>