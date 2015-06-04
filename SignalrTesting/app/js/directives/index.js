export default ngModule => {
    require("./signalr")(ngModule);
    require("./bar-chart")(ngModule);
};