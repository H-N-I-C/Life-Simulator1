public interface IContextConvert<FROM, TO>
{
	TO Convert(FROM from);
}
