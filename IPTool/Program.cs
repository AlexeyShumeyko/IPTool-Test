using IPTool;

var logFilePath = Startup.GetFilePathArg(args, Startup.FILE_LOG);
var outputFilePath = Startup.GetFilePathArg(args, Startup.FILE_OUTPUT);
var addressStart = Startup.GetFilePathArg(args, Startup.ADDRESS_START);
var addressMask = Startup.GetFilePathArg(args, Startup.ADDRESS_MASK);

var timeStart = Startup.GetTimeArg(args, Startup.TIME_START);
var timeEnd = Startup.GetTimeArg(args, Startup.TIME_END);

if (string.IsNullOrEmpty(logFilePath) || string.IsNullOrEmpty(outputFilePath) 
    || timeStart == DateTime.MinValue || timeEnd == DateTime.MinValue)
{
    Console.WriteLine("Missing required parameters");
    return;
}