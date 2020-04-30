var map, datasource, popup, symbolLayer;

async function GetMap() {
    let response = await fetch('https://restaurantedb.azurewebsites.net/api/HttpTrigger1');
    let respJson = await response.json();
    var points = [];
    respJson.forEach(element => points.push(element));
    //Initialize a map instance.
    console.log(parseFloat(points[0].lat), parseFloat(points[0].lng))
    map = new atlas.Map('myMap', {
        center: [parseFloat(points[0].lng), parseFloat(points[0].lat)],
        zoom: 11,
        view: 'Auto',

        //Add your Azure Maps subscription key to the map SDK. Get an Azure Maps key at https://azure.com/maps
        authOptions: {
            authType: 'subscriptionKey',
            subscriptionKey: 'WcENk2Sv6IBBb1WQcZJ7ooj-I-GplPH0mIe6Ue4Uo4E'
        }
    });

    //Wait until the map resources are ready.
    map.events.add('ready', async function () {

        //Create a data source and add it to the map.
        datasource = new atlas.source.DataSource();
        map.sources.add(datasource);


        for (var i = 0; i < points.length; i++) {
            console.log(points[i]);
            var aux = new atlas.data.Feature(new atlas.data.Point([points[i].lng, points[i].lat]), {
                name: points[i].name,
                description: points[i].description,
                img: points[i].photo
            });
            console.log(aux);
            datasource.add(aux);
        }

        //Add a layer for rendering point data as symbols.
        symbolLayer = new atlas.layer.SymbolLayer(datasource, null, { iconOptions: { allowOverlap: true } });
        map.layers.add(symbolLayer);

        //Create a popup but leave it closed so we can update it and display it later.
        popup = new atlas.Popup({
            position: [0, 0],
            pixelOffset: [0, -18]
        });

        /**
         * Open the popup on mouse move or touchstart on the symbol layer.
         * Mouse move is used as mouseover only fires when the mouse initially goes over a symbol.
         * If two symbols overlap, moving the mouse from one to the other won't trigger the event for the new shape as the mouse is still over the layer.
         */
        map.events.add('mousemove', symbolLayer, symbolHovered);
        map.events.add('touchstart', symbolLayer, symbolHovered);

        //Close the popup on mouseout or touchend.
        map.events.add('mouseout', symbolLayer, closePopup);
        map.events.add('touchend', closePopup);
    });
}

function closePopup() {
    popup.close();
}

function symbolHovered(e) {
    //Make sure the event occurred on a shape feature.
    if (e.shapes && e.shapes.length > 0) {
        var properties = e.shapes[0].getProperties();

        //Update the content and position of the popup.
        popup.setOptions({
            //Create the content of the popup.
            content: `<div style="padding:10px;"><img src=${properties.img} style="height:100px;width:100px"><br><b>${properties.name}</b><br/>${properties.description}</div>`,
            position: e.shapes[0].getCoordinates(),
            pixelOffset: [0, -18]
        });

        //Open the popup.
        popup.open(map);
    }

}