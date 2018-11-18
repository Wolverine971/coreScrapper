<template>
    <v-layout column>
        <div v-for="(result) in Results" :key="result">
        </div>
    </v-layout>
</template>
<script>
export default {
    name: 'ScrapeResults',
    computed:{
        Results(){
            return this.$store.state.scrapeResults
        }
    }
}
</script>
