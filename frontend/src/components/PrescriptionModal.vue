<template>
    <div @click="$emit('closePrescriptionModal')" class="overlay"></div>
    <dialog open>
        <div class="outer-wrapper">
            <img class="info-icon" src="@/assets/images/icons/svg/base_card_info.svg">
            <img class="close-button" @click="$emit('closePrescriptionModal')" src="@/assets/images/icons/svg/base_modal_close.svg">
            <div class="inner-wrapper">
                <p class="title">Recepta z dnia {{ new Date(data.date).toLocaleDateString('pl', { year:"numeric", month:"long", day:"numeric"}) }}</p>
                <p class="subtitle">{{ data.drug }}</p>
                <div class="content">
                    <p><span>Odpłatność: </span> 100%</p>
                    <p><span>Dawkowanie: </span> {{ data.dosage }}</p>
                    <p><span>Kod recepty: </span> {{ data.prescriptionCode }}</p>
                    <p><span>Wystawca: </span> {{ data.doctor.user.firstName }} {{ data.doctor.user.lastName }} </p>
                    <p><span>Zalecenia: </span> {{ data.recommendations }}</p>
                </div>
            </div>
        </div>
    </dialog>
</template>
<script>
export default {
    name: 'PrescriptionModal',
    emits: ['closePrescriptionModal'],
    props: {
        data: {
            type: Object,
            default: function(){
                return {}
            }
        }
    }
}
</script>

<style lang="scss" scoped>
div.overlay {
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    width: 100%;
    z-index: 1;
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
    z-index: 2;
    div.outer-wrapper {
        border: 1.5px solid $secondary;
        background-color: $base-modal-background;
        border-radius: 10px;
        filter: drop-shadow(0 0 30px #999);
        padding: 35px;
        position: relative;

        img.info-icon {
            position: absolute;
            top: 30px;
            left: 30px;
        }

        img.close-button {
            width: 30px;
            position: absolute;
            top: 30px;
            right: 30px;
            cursor: pointer;
        }

        div.inner-wrapper {
            padding: 25px 50px;
            text-align: left;
            color: $secondary;

            p {
                margin-bottom: 0;
                font-size: 30px;

                &.title {
                    font-weight: 700;
                }
                &.subtitle {
                    text-transform: uppercase;
                }
            }
            

            div.content {
                p {
                    margin: 5px 0;
                    font-size: 20px;
                    span {
                        font-weight: 600;
                    }
                }
            }
        }

    }
}
</style>