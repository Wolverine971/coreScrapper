<template>
    <v-layout column>
        <div v-for="(result, index) in Results" :key="index">
            <span>{{result}}</span>
        </div>
    </v-layout>
</template>
<script>
export default {
    name: 'ScrapeResults',
    computed:{
        Results(){
            console.log("got the results")
            if(this.$store.state.scrapeResults)
            return this.$store.state.scrapeResults.scrappedThing
        }
    }
}
</script>
