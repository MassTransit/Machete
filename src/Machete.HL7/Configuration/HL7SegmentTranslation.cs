﻿namespace Machete.HL7
{
    public abstract class HL7SegmentTranslation<TResult, TInput, TSchema> :
        HL7EntityTranslation<TResult, TInput, TSchema>
        where TSchema : HL7Entity
        where TInput : TSchema, HL7Segment
        where TResult : TSchema, HL7Segment
    {
        protected HL7SegmentTranslation()
        {
            Exclude(x => x.SegmentId);
        }
    }


    public abstract class HL7SegmentTranslation<TSegment, TSchema> :
        HL7SegmentTranslation<TSegment, TSegment, TSchema>
        where TSchema : HL7Entity
        where TSegment : TSchema, HL7Segment
    {
    }
}